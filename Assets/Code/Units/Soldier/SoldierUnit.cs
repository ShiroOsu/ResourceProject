using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SoldierUnit : MonoBehaviour, IUnit
{
    public SoldierStats m_Stats;
    private NavMeshAgent m_Agent;

    private void Awake()
    {
        if (!m_Stats)
        { m_Stats = ScriptableObject.CreateInstance<SoldierStats>(); }

        m_Agent = GetComponent<NavMeshAgent>();

        m_Agent.agentTypeID = -1372625422; // ???
        m_Agent.speed = m_Stats.movementSpeed;
        m_Agent.acceleration = m_Stats.acceleration;
    }

    public void Destroy()
    {
        Debug.Log(transform.name + " Destroy");
    }

    public void Spawn()
    {
        Debug.Log(transform.name + " Spawn");
    }

    public void Move(Vector3 destination)
    {
        m_Agent.SetDestination(destination);
    }
}