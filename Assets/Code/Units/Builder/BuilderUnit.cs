using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BuilderUnit : MonoBehaviour, IUnit
{
    [SerializeField] private GameObject m_SelectionCircle;
    private NavMeshAgent m_Agent;

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

    public int GetUnitID()
    {
        return m_Agent.agentTypeID;
    }

    public void Destroy()
    {
        Destroy();
    }

    public void OnStructureBuildButton(StructureType type)
    {
        switch (type)
        {
            case StructureType.Castle:
                BuildManager.Instance.InitBuild(type);
                break;
            case StructureType.Barracks:
                BuildManager.Instance.InitBuild(type);
                break;
            default:
                break;
        }
    }

    public void Move(Vector3 destination)
    {
        m_Agent.SetDestination(destination);
    }
}
