using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class SoldierUnit : MonoBehaviour, IUnit
{
    public SoldierStats m_Stats;
    private CapsuleCollider m_UnitCollider;
    private NavMeshAgent m_Agent;
    public NavMeshAgent Agent => m_Agent;

    private IEnumerator m_MoveCoroutine;

    private void Awake()
    {
        if (!m_Stats)
        { m_Stats = ScriptableObject.CreateInstance<SoldierStats>(); }

        m_UnitCollider = GetComponent<CapsuleCollider>();
        m_Agent = GetComponent<NavMeshAgent>();

        m_Agent.agentTypeID = -1372625422;
        m_Agent.speed = m_Stats.movementSpeed;

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

        //m_MoveCoroutine = SmoothStep(transform.position, destination, m_Agent.speed);
        //StartCoroutine(m_MoveCoroutine);                    
    }

    private IEnumerator SmoothStep(Vector3 start, Vector3 end, float speed)
    {
        end.y += (m_UnitCollider.height * 0.5f);

        float startTime = Time.time;
        bool moving = true;
        float errorMargin = 0.001f;

        while (moving)
        {
            float currentSpeed = Mathf.Clamp01(Time.time - startTime) *
                Mathf.Clamp01((end - transform.position).magnitude) * speed * Time.deltaTime;

            transform.position += (end - start).normalized * currentSpeed;

            if ((end - transform.position).sqrMagnitude < errorMargin)
            {
                moving = false;
                transform.position = end;
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}