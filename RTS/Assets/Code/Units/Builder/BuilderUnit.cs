using System;
using System.Collections;
using Code.Interfaces;
using Code.Managers;
using Code.Managers.Building;
using Code.Resources;
using Code.SaveSystem.Data;
using Code.Tools.Debugging;
using Code.Tools.Enums;
using Code.Tools.HelperClasses;
using Code.UI;
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
        private ShopManager m_Shop;
        
        private void Awake()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            m_Shop = ShopManager.Instance;
            
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
                    if (!ShopManager.Instance.CanAffordBuilding(type))
                    {
                        Log.Print("BuilderUnit.cs", "Could not afford to create a Castle building!");
                        return;
                    }
                    BuildManager.Instance.InitBuild(type);
                    break;
                case StructureType.Barracks:
                    if (!ShopManager.Instance.CanAffordBuilding(type))
                    {
                        Log.Print("BuilderUnit.cs", "Could not afford to create a Barracks building!");
                        return;
                    }
                    BuildManager.Instance.InitBuild(type);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void MoveToResource(Vector3 destination, IResource resource)
        {
            m_Agent.SetDestination(destination);
            StartCoroutine(HasAgentReachedDestination(destination, resource));
        }
        
        public void Move(Vector3 destination)
        {
            m_Agent.SetDestination(destination);
        }

        public bool IsUnitMoving()
        {
            return !m_Agent.velocity.Equals(Vector3.zero);
        }

        private IEnumerator HasAgentReachedDestination(Vector3 dest, IResource resource)
        {
            var curDist = Vector3.Distance(m_Agent.transform.position, dest);
            const float distAwayFromResource = 4f;
            
            while (curDist > distAwayFromResource)
            {
                curDist = Vector3.Distance(m_Agent.transform.position, dest);
                yield return null;
            }
            
            if (curDist <= distAwayFromResource)
            {
                ReachedResource(resource);
            }

            yield return null;
        }

        private void ReachedResource(IResource resource)
        {
            var added = resource.AddWorkers(GetUnitTexture());
            StopAgent(true);

            if (!added)
            {
                return;
            }
            
            ShouldSelect(false);
            gameObject.SetActive(false);
        }
        
        public void StopAgent(bool stop)
        {
            m_Agent.isStopped = stop;
        }

        public void Save()
        {
            m_BuilderData.Save(gameObject);
            SaveData.Instance.builderData.Add(m_BuilderData);
        }
    }
}
