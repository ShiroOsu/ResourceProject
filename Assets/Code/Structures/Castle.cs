using UnityEngine;

public class Castle : MonoBehaviour, IStructure
{
    [Header("Game Manager")]
    [SerializeField] private GameManager m_GameManager;
    
    [Header("Image in UI")]
    [SerializeField] private GameObject m_CastleImage = null;

    // Spawn location for builders
    private Vector3 m_UnitSpawnLocation;

    private void Awake()
    {
        // If possible to build castles, when building make sure it has the game manager,
        // if manager has the builder objectPool
        if (!m_GameManager) { m_GameManager = FindObjectOfType<GameManager>(); }
    }

    public void SetUnitSpawnPoint(Vector3 spawnPoint)
    {
        m_UnitSpawnLocation = spawnPoint;
    }

    public void Destroy()
    {
        // Temp
        Destroy();
    }

    public void Unselect()
    {
        m_CastleImage.SetActive(false);
    }

    public void Selected()
    {
        m_CastleImage.SetActive(true);
    }

    private void SpawnBuilder()
    {
        m_GameManager.m_BuilderPool.Rent(true).transform.position = transform.position;
    }
    
    public void Upgrade()
    {
        Debug.Log(transform.name + " upgrade");
    }
}
