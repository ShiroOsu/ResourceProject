using Code.Framework.Enums;
using Code.Framework.Interfaces;
using Code.Managers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Code.Units.Horse
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class HorseUnit : MonoBehaviour, IUnit
    {
        [SerializeField] private GameObject selectionCircle;
        private NavMeshAgent m_Agent;

        private void Awake()
        {
            m_Agent = GetComponent<NavMeshAgent>();

            m_Agent.speed = DataManager.Instance.unitData.movementSpeed;
            m_Agent.acceleration = DataManager.Instance.unitData.acceleration;
        }

        public void ShouldSelect(bool select)
        {
            UIManager.Instance.UnitSelected(select, gameObject);
            ActivateSelectionCircle(select);
        }

        public void ActivateSelectionCircle(bool active)
        {
            selectionCircle.SetActive(active);
        }

        public void OnDestroy()
        {
            Destroy(this);
        }

        public TextureAssetType GetUnitType()
        {
            return TextureAssetType.Horse;
        }

        public void Move(Vector3 destination)
        {
            m_Agent.SetDestination(destination);
        }
    }
}
