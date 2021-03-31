using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(NavMeshAgent))]
public class DebugAgentPath : MonoBehaviour
{
    private LineRenderer m_Line;
    private NavMeshAgent m_Agent;

    private void Start()
    {
        m_Line = GetComponent<LineRenderer>();
        m_Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (m_Agent.hasPath)
        {
            m_Line.positionCount = m_Agent.path.corners.Length;
            m_Line.SetPositions(m_Agent.path.corners);
            m_Line.enabled = true;
        }
        else
        {
            m_Line.enabled = false;
        }
    }
}