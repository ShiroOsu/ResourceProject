using UnityEngine;

public class SoldierUnit : MonoBehaviour, IUnit
{
    public SoldierStats m_Stats;

    private float m_Speed = 1f;
    private bool move = false;
    private Vector3 m_Pos;

    private void Awake()
    {
        if (!m_Stats)
        { m_Stats = ScriptableObject.CreateInstance<SoldierStats>(); }
    }

    public void Update()
    {
        if (move)
        { MoveUnits(Time.deltaTime); }
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
        // Temp
        m_Pos = destination;
        m_Pos.y = transform.position.y; // do not change y pos
        move = true;
    }

    private void MoveUnits(float deltaTime)
    {
        transform.position = Vector3.Lerp(transform.position, m_Pos, m_Speed * deltaTime);

        if (transform.position == m_Pos)
        {
            move = false;
        }
    }
}