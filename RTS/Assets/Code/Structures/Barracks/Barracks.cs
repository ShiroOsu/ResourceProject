using System;
using Code.Framework;
using Code.Framework.Enums;
using Code.Framework.Interfaces;
using Code.Managers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Code.Structures.Barracks
{
    public class Barracks : MonoBehaviour, IStructure
    {
        [SerializeField] private NavMeshObstacle m_NavMeshObstacle;
        [SerializeField] private CustomSizer3D m_Sizer3D;
        public float m_SpawnTimeSoldier;
        public float m_SpawnTimeHorse;
        public Transform m_UnitSpawnPoint;
        public event Action<UnitType> OnSpawn;

        public Vector3 FlagPoint { get; private set; }
        private GameObject m_Flag = null;
        private bool m_SetSpawnFlag = false;
        public BarracksTimer BarracksTimer { get; set; }
        public bool HasCreateTimer { get; set; }

        private void Awake()
        {
            m_NavMeshObstacle.shape = NavMeshObstacleShape.Box;
            m_NavMeshObstacle.size = m_Sizer3D.GetSize(gameObject.transform.lossyScale);
        }

        private void Update()
        {
            if (BarracksTimer)
            {
                BarracksTimer.TimerUpdate();
            }
            
            if (Mouse.current.rightButton.isPressed)
            {
                m_SetSpawnFlag = false;
            }

            if (m_SetSpawnFlag)
            {
                SetFlagPosition();
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
            OnSpawn?.Invoke(UnitType.Solider);
        }

        public void SpawnHorse()
        {
            OnSpawn?.Invoke(UnitType.Horse);
        }

        public void ShouldSelect(bool select)
        {
            UIManager.Instance.StructureSelected(StructureType.Barracks, select, gameObject);
            BarracksTimer.Barracks = this;
            BarracksTimer.ShowTimer(false);
            BarracksTimer.AddActionOnSpawn(select);

            if (m_Flag != null)
                m_Flag.SetActive(select);

            if (!select && m_SetSpawnFlag)
            {
                m_SetSpawnFlag = false;
            }
        }

        public void Upgrade()
        {
        }
        
        public void Destroy()
        {}
    }
}
