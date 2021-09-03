using System;
using Code.Framework.Enums;
using Code.Framework.Interfaces;
using Code.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Units.Horse
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class HorseUnit : MonoBehaviour, IUnit
    {
        [SerializeField] private GameObject m_SelectionCircle;
        private NavMeshAgent m_Agent;

        private void Awake()
        {
            m_Agent = GetComponent<NavMeshAgent>();

            m_Agent.agentTypeID = DataManager.Instance.unitData.horseID;
            m_Agent.speed = DataManager.Instance.unitData.movementSpeed;
            m_Agent.acceleration = DataManager.Instance.unitData.acceleration;
        }

        public void ShouldSelect(bool select)
        {
            UIManager.Instance.UnitSelected(UnitType.Horse, select, gameObject);
            ActivateSelectionCircle(select);
        }

        public void ActivateSelectionCircle(bool active)
        {
            m_SelectionCircle.SetActive(active);
        }

        public void OnDestroy()
        {
            Destroy(this);
        }

        public int GetUnitID()
        {
            return m_Agent.agentTypeID;
        }

        public void Move(Vector3 destination)
        {
            m_Agent.SetDestination(destination);
        }
    }
}
