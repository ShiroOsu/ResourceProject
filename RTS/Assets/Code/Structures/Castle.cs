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
    public class Castle : MonoBehaviour, IStructure
    {
        [SerializeField] private NavMeshObstacle navMeshObstacle;
        [SerializeField] private CustomSizer3D sizer3D;
        public Transform unitSpawnPoint;
        [SerializeField] private GameObject outlineRenderer;
        [SerializeField] private StructureData data;
        private GameObject m_StructureImage;
        public GameObject castleUIMiddle;
        private const string c_NameOfUIObjectInScene = "CastleUIMiddle";
        public string NameOfUIObjectInScene => c_NameOfUIObjectInScene;

        public Vector3 FlagPoint { get; private set; }
        private GameObject m_Flag = null;
        private bool m_SetSpawnFlag = false;
        public event Action<TextureAssetType> OnSpawn;
        public CastleTimer castleTimer;

        private void Awake()
        {
            navMeshObstacle.shape = NavMeshObstacleShape.Box;
            navMeshObstacle.size = sizer3D.GetSize(gameObject.transform.lossyScale);
            
            if (!m_StructureImage)
            {
                m_StructureImage = Extensions.FindObject("CastleImage");
            }

            if (!castleUIMiddle)
            {
                castleUIMiddle = Extensions.FindObject(c_NameOfUIObjectInScene);
            }
        }

        private void Update()
        {
            if (castleTimer)
            {
                castleTimer.TimerUpdate();
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

        public void OnSpawnBuilderButton()
        {
            OnSpawn?.Invoke(TextureAssetType.Builder);
        }

        public void Destroy()
        {
            Destroy(this);
        }

        public void Upgrade()
        {
            Debug.Log(transform.name + " upgrade");
        }

        public void ShouldSelect(bool select)
        {
            UIManager.Instance.StructureSelected(select, gameObject, StructureType.Castle, m_StructureImage, data);
            castleTimer.Castle = this;
            castleTimer.AddActionOnSpawn(select);
            outlineRenderer.SetActive(select);

            if (!select)
            {
                castleTimer.timer.transform.SetParent(transform);
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
            return StructureType.Castle;
        }

        public GameObject GetStructureImage()
        {
            return m_StructureImage;
        }
    }
}