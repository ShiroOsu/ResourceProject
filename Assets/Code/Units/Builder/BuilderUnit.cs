using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class BuilderUnit : MonoBehaviour, IUnit
{
    [SerializeField] private UnitData m_Data = null;
    [SerializeField] private GameObject m_SelectionCircle;

    [Header("UI")]
    [SerializeField] private GameObject m_BuilderImage = null;
    [SerializeField] private GameObject m_BuilderUI = null;
    [SerializeField] private GameObject m_BuilderPage = null;
    [SerializeField] private GameObject m_BuilderMainPage = null;
    [SerializeField] private GameObject m_MainHealthBar = null;
    [SerializeField] private TextMeshPro m_HealthNumbers = null;

    private NavMeshAgent m_Agent;
    private float m_MaxHealth;
    private float m_CurrentHealth;
    private MouseInputs m_MouseInputs = null;
    private bool m_ShowStructurePlacement;
    private GameObject m_StructureToBuild = null;

    private void Awake()
    {
        m_Data ??= ScriptableObject.CreateInstance<UnitData>();
        m_MouseInputs ??= ReferenceHolder.Instance.MouseControls.GetComponent<MouseInputs>();

        m_Agent = GetComponent<NavMeshAgent>();

        m_Agent.agentTypeID = 0; // Builder ID
        m_Agent.speed = m_Data.movementSpeed;
        m_Agent.acceleration = m_Data.acceleration;
        //m_Agent.angularSpeed = m_Data.turnSpeed;

        m_BuilderImage = ReferenceHolder.Instance.BuilderImage;
        m_BuilderUI = ReferenceHolder.Instance.BuilderUI;
        m_BuilderMainPage = ReferenceHolder.Instance.BuilderMainPage;
        m_BuilderPage = ReferenceHolder.Instance.BuilderPage;
        m_MainHealthBar = ReferenceHolder.Instance.MainHealthBar;                
        m_HealthNumbers = ReferenceHolder.Instance.HealthNumbers;

        m_MaxHealth = m_Data.maxHealth;
        m_CurrentHealth = m_MaxHealth;
    }

    public void UnSelect()
    {
        EnableUI(false);
    }

    public void Select()
    {
        UIManager.Instance.UnitSelected(UnitType.Builder);

        //m_HealthNumbers.SetText(m_CurrentHealth.ToString());
        //EnableUI(true);
    }

    public void Destroy()
    {
        // ?
        gameObject.SetActive(false);
    }

    private void Update()
    {
        //Debug.Log(m_ShowStructurePlacement);

        if (m_ShowStructurePlacement)
        {
            BuildStructure();
        }
    }

    public void BuildStructure()
    {
        Ray ray = m_MouseInputs.PlacementRay;

        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            var groundPoint = hit.point;

            m_StructureToBuild.transform.position = groundPoint;

            if (Mouse.current.rightButton.isPressed || Mouse.current.leftButton.isPressed)
            {
                m_StructureToBuild.transform.position = groundPoint;
                //m_ShowStructurePlacement = false;
            }
        }
    }

    public void OnStructureBuildButton(GameObject structure)
    {
        m_ShowStructurePlacement = true;
        m_StructureToBuild = structure;
        Instantiate(m_StructureToBuild);
    }

    public void Move(Vector3 destination)
    {
        m_Agent.SetDestination(destination);
    }

    private void EnableUI(bool active)
    {
        m_SelectionCircle.SetActive(active);
        m_BuilderImage.SetActive(active);
        m_BuilderUI.SetActive(active);
        m_MainHealthBar.SetActive(active); // Temp
    }

    public void BuildButton()
    {
        BuildAndMainPageActivation(true);
    }

    public void BackButton()
    {
        BuildAndMainPageActivation(false);
    }

    public void BuildAndMainPageActivation(bool active)
    {
        m_BuilderPage.SetActive(active);
        m_BuilderMainPage.SetActive(!active);
    }
}