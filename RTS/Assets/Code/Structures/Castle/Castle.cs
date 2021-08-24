using System;
using Code.Framework.Enums;
using Code.Framework.Interfaces;
using Code.Logger;
using Code.Managers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Code.Structures.Castle
{
    public class Castle : MonoBehaviour, IStructure
    {
        [SerializeField] private NavMeshObstacle m_NavMeshObstacle;
        [SerializeField] private CustomSizer3D m_Sizer3D;
        
        // Spawn location for builders
        private Vector3 m_UnitSpawnPoint;
        private GameObject m_Flag = null;
        private bool m_SetSpawnFlag = false;

        private void Awake()
        {
            m_NavMeshObstacle.shape = NavMeshObstacleShape.Box;
            m_NavMeshObstacle.size = m_Sizer3D.GetSize(gameObject.transform.lossyScale);
        }

        private void Update()
        {
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
            m_UnitSpawnPoint = m_Flag.transform.position;
        }

        public void OnSetSpawnFlagPosition()
        {
            m_Flag ??= FlagManager.Instance.InstaniateNewFlag();
            m_SetSpawnFlag = true;
        }

        public void OnSpawnBuilderButton()
        {
            SpawnManager.Instance.SpawnUnit(UnitType.Builder, gameObject.transform.position, m_UnitSpawnPoint);
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
            UIManager.Instance.StructureSelected(StructureType.Castle, select, gameObject);

            if (m_Flag != null)
                m_Flag.SetActive(select);

            if (!select && m_SetSpawnFlag)
            {
                m_SetSpawnFlag = false;
            }
        }
    }
}