using System.Collections;
using Code.Interfaces;
using Code.SaveSystem.Data;
using Code.ScriptableObjects;
using Code.Tools.Enums;
using Code.Tools.HelperClasses;
using Code.UI;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Units.Worker
{
    public class WorkerUnit : BaseUnit
    {
        [SerializeField] private GameObject selectionCircle;
        [SerializeField] private UnitData unitData;
        [SerializeField] private GameObject fovObject;
        
        private GameObject m_UnitImage;
        private readonly WorkerData m_WorkerData = new();

        private void Awake()
        {
            UnitType = UnitType.Worker;
            TextureAssetType = TextureAssetType.Worker;
            
            fovObject.transform.localScale = new Vector3(unitData.fieldOfView, 0f, unitData.fieldOfView);
            
            Agent = GetComponent<NavMeshAgent>();
            Agent.speed = unitData.movementSpeed;
            Agent.acceleration = unitData.acceleration;
            
            if (!m_UnitImage)
            {
                m_UnitImage = Extensions.FindObject("WorkerImage");
            }
        }

        public override void EnableFoV(bool fov = true)
        {
            fovObject.SetActive(fov);
        }

        public override GameObject GetUnitImage()
        {
            return m_UnitImage;
        }

        public void MoveToResource(Vector3 destination, IResource resource)
        {
            Agent.SetDestination(destination);
            StartCoroutine(HasAgentReachedDestination(destination, resource));
        }

        private IEnumerator HasAgentReachedDestination(Vector3 dest, IResource resource)
        {
            var curDist = Vector3.Distance(Agent.transform.position, dest);
            const float distAwayFromResource = 4f;
            
            while (curDist > distAwayFromResource)
            {
                curDist = Vector3.Distance(Agent.transform.position, dest);
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

        public override void ShouldSelect(bool select)
        {
            UIManager.Instance.UnitSelected(select, gameObject, UnitType.Worker, m_UnitImage, unitData);
            ActivateSelectionCircle(select);
        }

        public override void ActivateSelectionCircle(bool active)
        {
            selectionCircle.SetActive(active);
        }

        public override void Save()
        {
            m_WorkerData.Save(gameObject);
            SaveData.Instance.workerData.Add(m_WorkerData);
        }
    }
}
