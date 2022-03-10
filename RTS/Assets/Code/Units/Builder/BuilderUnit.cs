using System;
using Code.Enums;
using Code.HelperClasses;
using Code.Interfaces;
using Code.Managers.Building;
using Code.Managers.UI;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Units.Builder
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BuilderUnit : MonoBehaviour, IUnit
    {
        [SerializeField] private GameObject selectionCircle;
        [SerializeField] private UnitData data;
        private GameObject m_UnitImage;
        private NavMeshAgent m_Agent;

        private void Awake()
        {
            m_Agent = GetComponent<NavMeshAgent>();

            m_Agent.speed = data.movementSpeed;
            m_Agent.acceleration = data.acceleration;

            if (!m_UnitImage)
            {
                m_UnitImage = Extensions.FindObject("BuilderImage");
            }
        }

        public void ShouldSelect(bool select)
        {
            UIManager.Instance.UnitSelected(select, gameObject, UnitType.Builder, m_UnitImage, data);
            ActivateSelectionCircle(select);
        }

        public void ActivateSelectionCircle(bool active)
        {
            selectionCircle.SetActive(active);
        }

        public UnitType GetUnitType()
        {
            return UnitType.Builder;
        }
        
        public TextureAssetType GetUnitTexture()
        {
            return TextureAssetType.Builder;
        }

        public UnitData GetUnitData()
        {
            return data;
        }

        public GameObject GetUnitImage()
        {
            return m_UnitImage;
        }

        public void OnDestroy()
        {
            Destroy(this);
        }

        public void OnStructureBuildButton(StructureType type)
        {
            switch (type)
            {
                case StructureType.Castle:
                    BuildManager.Instance.InitBuild(type);
                    break;
                case StructureType.Barracks:
                    BuildManager.Instance.InitBuild(type);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        public void Move(Vector3 destination)
        {
            m_Agent.SetDestination(destination);
        }

        public bool IsUnitMoving()
        {
            return !m_Agent.velocity.Equals(Vector3.zero);
        }

        public Vector3Int GetPosition()
        {
            return Extensions.Vector3ToVector3Int(gameObject.transform.position);
        }
    }
}
