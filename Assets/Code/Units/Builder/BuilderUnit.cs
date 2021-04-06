using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BuilderUnit : MonoBehaviour, IUnit
{
    public BuilderStats m_Stats = null;
    private NavMeshAgent m_Agent;

    private void Awake()
    {
        if (!m_Stats) 
        { m_Stats = ScriptableObject.CreateInstance<BuilderStats>(); }
        
        m_Agent = GetComponent<NavMeshAgent>();

        m_Agent.agentTypeID = 0; // Builder ID
        m_Agent.speed = m_Stats.movementSpeed;
        m_Agent.acceleration = m_Stats.acceleration;
        //m_Agent.angularSpeed = m_Stats.turnSpeed;
    }

    public void Selected()
    {
        Debug.Log(transform.name + " selected");
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    public void Build()
    {
        // When this unit is ordered to build a structure move towards the location
        // Move(Structure.location)
    }

    // Regular move
    public void Move(Vector3 destination)
    {
        m_Agent.SetDestination(destination);
    }
}