using System;
using Code.Framework.Enums;
using Code.Framework.Interfaces;
using Code.Framework.Logger;
using Code.Framework.Timers;
using Code.Framework.Tools;
using Code.Managers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Code.Structures.Castle
{
    public class Castle : MonoBehaviour, IStructure
    {
        [SerializeField] private NavMeshObstacle navMeshObstacle;
        [SerializeField] private CustomSizer3D sizer3D;
        public Transform unitSpawnPoint;
        [SerializeField] private GameObject outlineRenderer;

        public Vector3 FlagPoint { get; private set; }
        private GameObject m_Flag = null;
        private bool m_SetSpawnFlag = false;
        public event Action<TextureAssetType> OnSpawn;
        public CastleTimer castleTimer;

        private void Awake()
        {
            navMeshObstacle.shape = NavMeshObstacleShape.Box;
            navMeshObstacle.size = sizer3D.GetSize(gameObject.transform.lossyScale);
        }

        private void Update()
        {
            if (castleTimer)
            {
                castleTimer.TimerUpdate();
            }

            if (Mouse.current.rightButton.isPressed)
            {
                Log.Print("Castle.cs", "right mouse button pressed");
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
            UIManager.Instance.StructureSelected(StructureType.Castle, select, gameObject);
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
    }
}