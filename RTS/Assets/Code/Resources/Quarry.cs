using Code.Enums;
using Code.HelperClasses;
using Code.Interfaces;
using Code.Managers;
using Code.Managers.UI;
using Code.SaveSystem.Data;
using Code.ScriptableObjects;
using Code.Tools;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Resources
{
    public class Quarry : MonoBehaviour, IResource, ISavable
    {
        [SerializeField] private ResourceData data;
        [SerializeField] private NavMeshObstacle navMeshObstacle;
        [SerializeField] private CustomSizer3D sizer3D;
        [SerializeField] private GameObject outlineRenderer;
        public GameObject quarryUIMiddle;
        public Transform removedUnitsSpawnPos;
        public uint currentStoneLeft;
        public uint stoneLoadedFromData;
        public uint currentQuarryWorkers;
        public QuarryWorkers quarryWorkers;

        private readonly QuarryData m_QuarryData = new();
        private GameObject m_ResourceImage;
        private const string c_NameOfUIObjectInScene = "QuarryUIMiddle";
        private const string c_ResourceImage = "QuarryImage";
        private bool m_FirstSelect = true;

        private void Awake()
        {
            UpdateManager.Instance.OnUpdate += OnUpdate;
            
            //currentQuarryWorkers = m_QuarryData.quarryWorkers;
            
            navMeshObstacle.shape = NavMeshObstacleShape.Box;
            navMeshObstacle.size = sizer3D.GetSize(gameObject.transform.lossyScale);
            
            if (!m_ResourceImage)
            {
                m_ResourceImage = Extensions.FindObject(c_ResourceImage);
            }

            if (!quarryUIMiddle)
            {
                quarryUIMiddle = Extensions.FindObject(c_NameOfUIObjectInScene);
            }
        }
        
        private void OnUpdate()
        {
            if (!quarryWorkers)
            {
                return;
            }
            
            quarryWorkers.TimerUpdate();
        }
        
        public void ShouldSelect(bool select)
        {
            if (m_FirstSelect)
            {            
                currentStoneLeft = stoneLoadedFromData != 0 ? stoneLoadedFromData : data.resourcesLeft;
                data.resourcesLeft = currentStoneLeft;
                m_FirstSelect = false;
            }
            
            UIManager.Instance.ResourceSelected(select, gameObject, ResourceType.Stone, m_ResourceImage, data);
            quarryWorkers.Quarry = this;
            outlineRenderer.SetActive(select);

            if (!select)
            {
                quarryWorkers.gameObject.transform.SetParent(transform);
            }
        }

        public bool AddWorkers(TextureAssetType type)
        {
            return quarryWorkers.AddWorker(type);
        }

        public void ReduceResources(uint amount)
        {
            currentStoneLeft -= amount;
            data.resourcesLeft = currentStoneLeft;
            if (outlineRenderer.activeInHierarchy) // If it is selected
            {
                UIManager.Instance.SetResourceStatsInfo(data, true);
            }
        }

        public void Save()
        {
            m_QuarryData.Save(gameObject, currentStoneLeft, currentQuarryWorkers);
            SaveData.Instance.quarryData.Add(m_QuarryData);
        }
    }
}