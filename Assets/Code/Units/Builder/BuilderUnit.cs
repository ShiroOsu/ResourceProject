using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BuilderUnit : MonoBehaviour, IUnit
{
    [SerializeField] private UnitData m_Data = null;
    [SerializeField] private GameObject m_SelectionCircle;

    [Header("UI")]
    [SerializeField] private GameObject m_BuilderImage = null;
    [SerializeField] private GameObject m_BuilderUI = null;
    [SerializeField] private GameObject m_MainHealthBar = null;
    [SerializeField] private TextMeshPro m_HealthNumbers = null;
    
    private NavMeshAgent m_Agent;
    private float m_MaxHealth;
    private float m_CurrentHealth;

    private void Awake()
    {
        if (!m_Data) 
        { m_Data = ScriptableObject.CreateInstance<UnitData>(); }
        
        m_Agent = GetComponent<NavMeshAgent>();

        m_Agent.agentTypeID = 0; // Builder ID
        m_Agent.speed = m_Data.movementSpeed;
        m_Agent.acceleration = m_Data.acceleration;
        //m_Agent.angularSpeed = m_Data.turnSpeed;

        m_BuilderImage = ReferenceHolder.Instance.BuilderImage;
        m_BuilderUI = ReferenceHolder.Instance.BuilderUI;

        m_MainHealthBar = ReferenceHolder.Instance.MainHealthBar;
        m_HealthNumbers = ReferenceHolder.Instance.HealthNumbers;
        m_MaxHealth = m_Data.maxHealth;
        m_CurrentHealth = m_MaxHealth;
    }

    private void Update()
    {
    }

    public void Unselect()
    {
        m_SelectionCircle.SetActive(false);
        EnableUI(false);
    }

    public void Selected()
    {
        m_HealthNumbers.SetText(m_CurrentHealth.ToString());
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
        m_MainHealthBar.SetActive(active);
    }
}