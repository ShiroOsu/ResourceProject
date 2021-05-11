using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SoldierUnit : MonoBehaviour, IUnit
{
    [SerializeField] private GameObject m_SelectionCircle;
    private NavMeshAgent m_Agent;

    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();

        m_Agent.agentTypeID = DataManager.Instance.unitData.soldierID;
        m_Agent.speed = DataManager.Instance.unitData.movementSpeed;
        m_Agent.acceleration = DataManager.Instance.unitData.acceleration;
    }

    public void ShouldSelect(bool select)
    {
        UIManager.Instance.UnitSelected(UnitType.Solider, select, gameObject);
        m_SelectionCircle.SetActive(select);
    }

    public void Destroy()
    {
        Destroy();
    }

    public void Move(Vector3 destination)
    {
        m_Agent.SetDestination(destination);
    }
}
