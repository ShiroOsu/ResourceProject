using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class BuilderUnit : MonoBehaviour, IUnit
{
    [SerializeField] private GameObject m_SelectionCircle;

    private NavMeshAgent m_Agent;
    private bool m_ShowStructurePlacement;
    private GameObject m_StructureToBuild = null;

    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();

        m_Agent.agentTypeID = DataManager.Instance.unitData.builderID;
        m_Agent.speed = DataManager.Instance.unitData.movementSpeed;
        m_Agent.acceleration = DataManager.Instance.unitData.acceleration;
    }

    public void ShouldSelect(bool select)
    {
        UIManager.Instance.UnitSelected(UnitType.Builder, select, gameObject);
        m_SelectionCircle.SetActive(select);
    }

    public void Destroy()
    {
        Destroy();
    }

    private void Update()
    {
        if (m_ShowStructurePlacement)
        {
            BuildStructure();
        }
    }

    public void BuildStructure()
    {
        Ray ray = DataManager.Instance.mouseInputs.PlacementRay;

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
}