using System;
using System.Collections.Generic;
using Code.Interfaces;
using Code.Managers;
using Code.SaveSystem.Data;
using Code.ScriptableObjects;
using Code.Timers;
using Code.Tools;
using Code.Tools.Debugging;
using Code.Tools.Enums;
using Code.Tools.HelperClasses;
using Code.UI;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Structures
{
    public class Castle : MonoBehaviour, IStructure, ISavable, IFoV
    {
        [SerializeField] private NavMeshObstacle navMeshObstacle;
        [SerializeField] private CustomSizer3D sizer3D;
        public Transform unitSpawnPoint;
        [SerializeField] private GameObject outlineRenderer;
        [SerializeField] private GameObject fovObject;
        [SerializeField] private StructureData data;
        private GameObject m_StructureImage;
        public GameObject castleUIMiddle;
        private const string c_NameOfUIObjectInScene = "CastleUIMiddle";
        private const string c_CastleImage = "CastleImage";

        public Vector3 FlagPoint { get; set; }
        public GameObject Flag { get; set; }
        private bool m_SetSpawnFlag = false;
        public event Action<TextureAssetType> OnSpawn;
        public CastleTimer castleTimer;
        
        private readonly CastleData m_CastleData = new();
        
        private void Awake()
        {
            UpdateManager.Instance.OnUpdate += OnUpdate;
            navMeshObstacle.shape = NavMeshObstacleShape.Box;
            navMeshObstacle.size = sizer3D.GetSize(gameObject.transform.lossyScale);
            
            fovObject.transform.localScale = new Vector3(data.fieldOfView, 0f, data.fieldOfView); 

            if (!m_StructureImage)
            {
                m_StructureImage = Extensions.FindObject(c_CastleImage);
            }

            if (!castleUIMiddle)
            {
                castleUIMiddle = Extensions.FindObject(c_NameOfUIObjectInScene);
            }
        }

        private void OnUpdate()
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

        public void EnableFoV(bool fov = true)
        {
            fovObject.SetActive(fov);
        }

        private void SetFlagPosition()
        {
            FlagManager.Instance.SetFlagPosition(Flag);
            FlagPoint = Flag.transform.position;
        }

        public void OnSetSpawnFlagPosition()
        {
            Flag ??= FlagManager.Instance.InstantiateNewFlag();
            m_SetSpawnFlag = true;
        }

        public void OnSpawnBuilderButton()
        {
            if (QueueFull()) return;
            
            if (!ShopManager.Instance.CanAffordUnit(UnitType.Builder))
            {
                Log.Print("Castle.cs", "Could not afford a Builder unit!");
                return;
            }
            
            OnSpawn?.Invoke(TextureAssetType.Builder);
        }

        public void OnSpawnWorkerButton()
        {
            if (QueueFull()) return;

            if (!ShopManager.Instance.CanAffordUnit(UnitType.Worker))
            {
                Log.Print("Castle.cs", "Could not afford a Worker unit!");
                return;
            }
            
            OnSpawn?.Invoke(TextureAssetType.Worker);
        }

        public void Destroy()
        {
            Destroy(this);
        }

        public void Upgrade()
        {
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

            if (Flag != null)
                Flag.SetActive(select);

            if (!select && m_SetSpawnFlag)
            {
                m_SetSpawnFlag = false;
            }
        }
        
        private bool QueueFull()
        {
            if (!castleTimer.IsQueueFull) 
                return false;
            
            Log.Print("Castle.cs", "Could not buy unit, queue is full!");
            return true;
        }
        
        public void Save()
        {
            m_CastleData.Save(gameObject);
            m_CastleData.flagPosition = FlagPoint;
            SaveData.Instance.castleData.Add(m_CastleData);
        }
    }
}