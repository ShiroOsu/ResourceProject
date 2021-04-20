using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class BuilderUnit : MonoBehaviour, IUnit
{
    [SerializeField] private UnitData m_Stats = null;
    [SerializeField] private GameObject m_SelectionCircle;

    [Header("UI")]
    [SerializeField] private GameObject m_BuilderImage = null;
    [SerializeField] private GameObject m_BuilderUI = null;
    private NavMeshAgent m_Agent;

    private void Awake()
    {
        if (!m_Stats) 
        { m_Stats = ScriptableObject.CreateInstance<UnitData>(); }
        
        m_Agent = GetComponent<NavMeshAgent>();

        m_Agent.agentTypeID = 0; // Builder ID
        m_Agent.speed = m_Stats.movementSpeed;
        m_Agent.acceleration = m_Stats.acceleration;
        //m_Agent.angularSpeed = m_Stats.turnSpeed;

        m_BuilderImage = ReferenceHolder.Instance.BuilderImage;
        m_BuilderUI = ReferenceHolder.Instance.BuilderUI;
    }

    public void Unselect()
    {
        m_SelectionCircle.SetActive(false);
        EnableUI(false);
    }

    public void Selected()
    {
        m_SelectionCircle.SetActive(true);
        EnableUI(true);
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void Build()
    {
        // When this unit is ordered to build a structure move towards the location
        // Move(Structure.location)
    }

    // Regular move
    public void Move(Vector3 destination)
    {
        m_Agent.SetDestination(destination);
    }

    private void EnableUI(bool active)
    {
        m_BuilderImage.SetActive(active);
        m_BuilderUI.SetActive(active);
    }
}