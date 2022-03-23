using System;
using Code.Enums;
using Code.HelperClasses;
using Code.Interfaces;
using Code.Managers;
using Code.Managers.UI;
using Code.ScriptableObjects;
using Code.Timers;
using Code.Tools;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Structures
{
    public class Barracks : MonoBehaviour, IStructure
    {
        [SerializeField] private NavMeshObstacle navMeshObstacle;
        [SerializeField] private CustomSizer3D sizer3D;
        public Transform unitSpawnPoint;
        [SerializeField] private GameObject outlineRenderer;
        [SerializeField] private StructureData data;
        private GameObject m_StructureImage;
        public GameObject barracksUIMiddle;
        private const string c_NameOfUIObjectInScene = "BarracksUIMiddle";
        private const string c_BarracksImage = "BarracksImage";
        public event Action<TextureAssetType> OnSpawn;

        public Vector3 FlagPoint { get; private set; }
        private GameObject m_Flag = null;
        private bool m_SetSpawnFlag = false;
        public BarracksTimer barracksTimer;

        private void Awake()
        {
            navMeshObstacle.shape = NavMeshObstacleShape.Box;
            navMeshObstacle.size = sizer3D.GetSize(gameObject.transform.lossyScale);

            if (!m_StructureImage)
            {
                m_StructureImage = Extensions.FindObject(c_BarracksImage);
            }

            if (!barracksUIMiddle)
            {
                barracksUIMiddle = Extensions.FindObject(c_NameOfUIObjectInScene);
            }
        }

        private void Update()
        {
            if (barracksTimer)
            {
                barracksTimer.TimerUpdate();
            }
            
            if (m_SetSpawnFlag)
            {
                SetFlagPosition();

                if (Extensions.WasMousePressedThisFrame())
                {
                    m_SetSpawnFlag = false;
                }
            }
        }

        private void SetFlagPosition()
        {
            FlagManager.Instance.SetFlagPosition(m_Flag);
            FlagPoint = m_Flag.transform.position;
        }

        public void OnSetSpawnFlagPosition()
        {
            m_Flag ??= FlagManager.Instance.InstantiateNewFlag();
            m_SetSpawnFlag = true;
        }

        public void SpawnSoldier()
        {
            OnSpawn?.Invoke(TextureAssetType.Soldier);
        }

        public void SpawnHorse()
        {
            OnSpawn?.Invoke(TextureAssetType.Horse);
        }

        public void ShouldSelect(bool select)
        {
            UIManager.Instance.StructureSelected(select, gameObject, StructureType.Barracks, m_StructureImage, data);
            barracksTimer.Barracks = this;
            barracksTimer.AddActionOnSpawn(select);
            outlineRenderer.SetActive(select);

            if (!select)
            {
                barracksTimer.timer.transform.SetParent(transform);
            }

            if (m_Flag != null)
                m_Flag.SetActive(select);

            if (!select && m_SetSpawnFlag)
            {
                m_SetSpawnFlag = false;
            }
        }

        public StructureData GetStructureData()
        {
            return data;
        }

        public StructureType GetStructureType()
        {
            return StructureType.Barracks;
        }

        public GameObject GetStructureImage()
        {
            return m_StructureImage;
        }

        public void Upgrade()
        {
        }
        
        public void Destroy()
        {}
    }
}