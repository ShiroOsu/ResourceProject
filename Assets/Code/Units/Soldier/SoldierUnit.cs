using UnityEngine;
using System.Collections;

public class SoldierUnit : MonoBehaviour, IUnit
{
    // Why am I not using unity's Agent AI?

    public SoldierStats m_Stats;
    private CapsuleCollider m_UnitCollider;

    private float m_Speed;
    private bool m_Moving;
    private IEnumerator m_MoveCoroutine;

    private void Awake()
    {
        if (!m_Stats)
        { m_Stats = ScriptableObject.CreateInstance<SoldierStats>(); }

        m_Speed = m_Stats.movementSpeed;

        m_UnitCollider = GetComponent<CapsuleCollider>();
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
        // TODO / BUG
        // want to be able to move units to another location before reaching current

        // when spam-clicking on the ground the unit will get high speed
        // and move towards a different location then clicked, as if combining/adding new and lest vector

        m_MoveCoroutine = SmoothStep(transform.position, destination, m_Speed);
        StartCoroutine(m_MoveCoroutine);
    }

    private IEnumerator SmoothStep(Vector3 start, Vector3 end, float speed)
    {
        end.y += (m_UnitCollider.height * 0.5f);

        float startTime = Time.time;
        m_Moving = true;
        float errorMargin = 0.001f;

        while (m_Moving)
        {
            float currentSpeed = Mathf.Clamp01(Time.time - startTime) *
                Mathf.Clamp01((end - transform.position).magnitude) * speed * Time.deltaTime;

            transform.position += (end - start).normalized * currentSpeed;

            if ((end - transform.position).sqrMagnitude < errorMargin)
            {
                m_Moving = false;
                transform.position = end;
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}