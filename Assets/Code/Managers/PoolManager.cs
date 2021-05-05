using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // Singleton
    private static PoolManager s_Instance = null;
    public static PoolManager Instance => s_Instance ??= FindObjectOfType<PoolManager>();

    [Header("Pool Prefabs")]
    [SerializeField] private GameObject m_BuilderPrefab = null;
    [SerializeField] private GameObject m_SoldierPrefab = null;
    [SerializeField] private GameObject m_FlagPrefab = null;

    public ObjectPool builderPool = null;
    public ObjectPool soldierPool = null;
    public ObjectPool flagPool = null;

    private void Awake()
    {
        builderPool = new ObjectPool(5, m_BuilderPrefab, new GameObject("BuilderPool").transform);
        soldierPool = new ObjectPool(5, m_SoldierPrefab, new GameObject("SoliderPool").transform);
        flagPool = new ObjectPool(1, m_FlagPrefab, new GameObject("FlagPool").transform);
    }
}