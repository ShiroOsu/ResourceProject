using System;
using System.Collections;
using Code.Enums;
using Code.HelperClasses;
using Code.Interfaces;
using Code.Managers.Building;
using Code.Managers.Data;
using Code.Managers.UI;
using Code.SaveSystem.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Units.Builder
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BuilderUnit : MonoBehaviour, IUnit, ISavable
    {
        [SerializeField] private GameObject selectionCircle;
        [SerializeField] private UnitData data;
        private GameObject m_UnitImage;
        private NavMeshAgent m_Agent;

        private readonly BuilderData m_BuilderData = new();
        private DataManager m_DataManager;

        private void Awake()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            m_DataManager = DataManager.Instance;

            m_Agent.speed = data.movementSpeed;
            m_Agent.acceleration = data.acceleration;

            if (!m_UnitImage)
            {
                m_UnitImage = Extensions.FindObject("BuilderImage");
            }
        }

        public void ShouldSelect(bool select)
        {
            UIManager.Instance.UnitSelected(select, gameObject, UnitType.Builder, m_UnitImage, data);
            ActivateSelectionCircle(select);
        }

        public void ActivateSelectionCircle(bool active)
        {
            selectionCircle.SetActive(active);
        }

        public UnitType GetUnitType()
        {
            return UnitType.Builder;
        }
        
        public TextureAssetType GetUnitTexture()
        {
            return TextureAssetType.Builder;
        }

        public UnitData GetUnitData()
        {
            return data;
        }

        public GameObject GetUnitImage()
        {
            return m_UnitImage;
        }

        public GameObject GetUnitObject()
        {
            return gameObject;
        }

        public void OnDestroy()
        {
            Destroy(this);
        }

        public void OnStructureBuildButton(StructureType type)
        {
            switch (type)
            {
                case StructureType.Castle:
                    BuildManager.Instance.InitBuild(type);
                    break;
                case StructureType.Barracks:
                    BuildManager.Instance.InitBuild(type);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void MoveToResource(Vector3 destination, IResource resource)
        {
            m_Agent.SetDestination(destination);
            StartCoroutine(HasAgentReachedDestination(m_Agent, destination, resource));
        }
        
        public void Move(Vector3 destination)
        {
            m_Agent.SetDestination(destination);
        }

        public bool IsUnitMoving()
        {
            return !m_Agent.velocity.Equals(Vector3.zero);
        }

        private IEnumerator HasAgentReachedDestination(NavMeshAgent agent, Vector3 dest, IResource resource)
        {
            var curDist = Vector3.Distance(agent.transform.position, dest);
            const float distAwayFromEnd = 4f;
            
            while (curDist > distAwayFromEnd && agent.hasPath)
            {
                curDist = Vector3.Distance(agent.transform.position, dest);
                yield return null;
            }

            ReachedResource(resource);
            yield return null;
        }

        private void ReachedResource(IResource resource)
        {
            var added = resource.AddWorkerToMine(GetUnitTexture());
            m_Agent.SetDestination(m_Agent.transform.position);

            if (!added)
            {
                return;
            }

            var selectedUnits = m_DataManager.mouseInputs.SelectedUnitsList;
            if (selectedUnits.Contains(gameObject))
            {
                ShouldSelect(false);
                selectedUnits.Remove(gameObject);
                gameObject.SetActive(false);
                m_DataManager.mouseInputs.OnUpdateUnitListInvoker(selectedUnits);
            }
            resource.ShouldSelect(true);
            m_DataManager.mouseInputs.CurrentResource = resource;
        }

        public void Save()
        {
            m_BuilderData.Save(gameObject);
            SaveData.Instance.builderData.Add(m_BuilderData);
        }
    }
}
