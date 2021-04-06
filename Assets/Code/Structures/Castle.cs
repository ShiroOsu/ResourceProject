using UnityEngine;

public class Castle : MonoBehaviour, IStructure
{
    private GameManager m_GameManager;

    private void Awake()
    {
        m_GameManager = FindObjectOfType<GameManager>();
    }

    public void Destroy()
    {
        Destroy();
    }

    public void Selected()
    {
        Debug.Log(transform.name + " selected");
    }

    public void SpawnBuilder()
    {
        m_GameManager.m_BuilderPool.Rent(true);
    }
}
