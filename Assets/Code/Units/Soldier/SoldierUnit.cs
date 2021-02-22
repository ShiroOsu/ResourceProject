using UnityEngine;
using System.Collections;

public class SoldierUnit : MonoBehaviour, IUnit
{
    public SoldierStats m_Stats;

    private float m_Speed;
    private IEnumerator m_MoveCoroutine;

    private void Awake()
    {
        if (!m_Stats)
        { m_Stats = ScriptableObject.CreateInstance<SoldierStats>(); }

        m_Speed = m_Stats.movementSpeed;
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
        m_MoveCoroutine = SmoothStep(transform.position, destination, m_Speed);
        StartCoroutine(m_MoveCoroutine);
    }

    private IEnumerator SmoothStep(Vector3 start, Vector3 end, float speed)
    {
        // Temp
        end.y = start.y;

        float startTime = Time.time;
        bool moving = true;
        float errorMargin = 0.001f;

        while (moving)
        {
            float currentSpeed = Mathf.Clamp01(Time.time - startTime) * 
                Mathf.Clamp01(((end - transform.position).magnitude * speed) / speed) * speed * Time.deltaTime;

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