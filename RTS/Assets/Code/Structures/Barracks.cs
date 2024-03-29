using System;
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
    public class Barracks : MonoBehaviour, IStructure, ISavable, IFoV
    {
        [SerializeField] private NavMeshObstacle navMeshObstacle;
        [SerializeField] private CustomSizer3D sizer3D;
        public Transform unitSpawnPoint;
        [SerializeField] private GameObject outlineRenderer;
        [SerializeField] private StructureData data;
        [SerializeField] private GameObject fovObject;
        private GameObject m_StructureImage;
        public GameObject barracksUIMiddle;
        private const string c_NameOfUIObjectInScene = "BarracksUIMiddle";
        private const string c_BarracksImage = "BarracksImage";
        public event Action<TextureAssetType> OnSpawn;

        public Vector3 FlagPoint { get; set; }
        public GameObject Flag { get; set; }
        private bool m_SetSpawnFlag = false;
        public BarracksTimer barracksTimer;
        
        private readonly BarracksData m_BarracksData = new();

        private void Awake()
        {
            UpdateManager.Instance.OnUpdate += OnUpdate;
            navMeshObstacle.shape = NavMeshObstacleShape.Box;
            navMeshObstacle.size = sizer3D.GetSize(gameObject.transform.lossyScale);
            
            fovObject.transform.localScale = new Vector3(data.fieldOfView, 0f, data.fieldOfView);

            if (!m_StructureImage)
            {
                m_StructureImage = Extensions.FindObject(c_BarracksImage);
            }

            if (!barracksUIMiddle)
            {
                barracksUIMiddle = Extensions.FindObject(c_NameOfUIObjectInScene);
            }
        }

        private void OnUpdate()
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

        public void SpawnSoldier()
        {
            if (QueueFull()) return;
            
            if (!ShopManager.Instance.CanAffordUnit(UnitType.Soldier))
            {
                Log.Print("Barracks.cs", "Could not afford to create a Soldier unit");
                return;
            }
            OnSpawn?.Invoke(TextureAssetType.Soldier);
        }

        public void SpawnHorse()
        {
            if (QueueFull()) return;
            
            if (!ShopManager.Instance.CanAffordUnit(UnitType.Horse))
            {
                Log.Print("Barracks.cs", "Could not afford to create a Horse unit");
                return;
            }
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

            if (Flag != null)
                Flag.SetActive(select);

            if (!select && m_SetSpawnFlag)
            {
                m_SetSpawnFlag = false;
            }
        }

        public StructureType GetStructureType()
        {
            return StructureType.Barracks;
        }
       
        public void Upgrade()
        {
        }
        
        public void Destroy() {}

        private bool QueueFull()
        {
            if (!barracksTimer.IsQueueFull) 
                return false;
            
            Log.Print("Barracks.cs", "Could not buy unit, queue is full!");
            return true;
        }

        public void Save()
        {
            m_BarracksData.Save(gameObject);
            m_BarracksData.flagPosition = FlagPoint;
            SaveData.Instance.barracksData.Add(m_BarracksData);
        }
    }
}