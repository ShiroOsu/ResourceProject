using System;
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
    public class Goldmine : MonoBehaviour, IResource, ISavable
    {
        [SerializeField] private ResourceData data;
        [SerializeField] private NavMeshObstacle navMeshObstacle;
        [SerializeField] private CustomSizer3D sizer3D;
        [SerializeField] private GameObject outlineRenderer;
        public GameObject goldmineUIMiddle;
        public Transform removedUnitsSpawnPos;
        public uint currentGoldLeft;
        public uint goldLoadedFromData;
        public uint currentWorkersInMine;
        public GoldmineWorkers goldmineWorkers;

        private readonly GoldmineData m_GoldmineData = new();
        private GameObject m_ResourceImage;
        private const string c_NameOfUIObjectInScene = "GoldmineUIMiddle";
        private const string c_ResourceImage = "GoldmineImage";
        private bool m_FirstSelect = true;
        
        private void Awake()
        {
            UpdateManager.Instance.OnUpdate += OnUpdate;
            
            // This might be hard,
            // So I will not think about it before goldmine is working
            //currentWorkersInMine = m_GoldmineData.workersInMine;
            
            navMeshObstacle.shape = NavMeshObstacleShape.Box;
            navMeshObstacle.size = sizer3D.GetSize(gameObject.transform.lossyScale);
            
            if (!m_ResourceImage)
            {
                m_ResourceImage = Extensions.FindObject(c_ResourceImage);
            }

            if (!goldmineUIMiddle)
            {
                goldmineUIMiddle = Extensions.FindObject(c_NameOfUIObjectInScene);
            }
        }

        private void OnUpdate()
        {
            if (!goldmineWorkers)
            {
                return;
            }
            
            goldmineWorkers.TimerUpdate();
        }

        public bool AddWorkerToMine(TextureAssetType type)
        {
            return goldmineWorkers.AddWorker(type);
        }

        public void ShouldSelect(bool select)
        {
            if (m_FirstSelect)
            {            
                currentGoldLeft = goldLoadedFromData != 0 ? goldLoadedFromData : data.resourcesLeft;
                data.resourcesLeft = currentGoldLeft;
                m_FirstSelect = false;
            }
            
            UIManager.Instance.ResourceSelected(select, gameObject, ResourceType.Gold, m_ResourceImage, data);
            goldmineWorkers.Goldmine = this;
            outlineRenderer.SetActive(select);

            if (!select)
            {
                goldmineWorkers.gameObject.transform.SetParent(transform);
            }
        }

        public void ReduceResources(uint amount)
        {
            currentGoldLeft -= amount;
            data.resourcesLeft = currentGoldLeft;
            if (outlineRenderer.activeInHierarchy) // If it is selected
            {
                UIManager.Instance.SetResourceStatsInfo(data);
            }
        }
        
        public void Save()
        {
            m_GoldmineData.Save(gameObject, currentGoldLeft, currentWorkersInMine);
            SaveData.Instance.goldminesData.Add(m_GoldmineData);
        }
    }
}