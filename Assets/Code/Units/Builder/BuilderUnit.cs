using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BuilderUnit : MonoBehaviour, IUnit
{
    private NavMeshAgent m_Agent;

    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    public void Destroy()
    {
        Debug.Log(transform.name + " destroy");
    }

    public void Spawn()
    {
        Debug.Log(transform.name + " spawn");
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