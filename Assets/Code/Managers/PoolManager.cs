using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // Singleton
    private static PoolManager s_Instance = null;
    public static PoolManager Instance => s_Instance ??= FindObjectOfType<PoolManager>();

    [Header("Structures")]
    [SerializeField] private GameObject m_CastlePrefab = null;
    [SerializeField] private GameObject m_BarracksPrefab = null;

    [Header("Units")]
    [SerializeField] private GameObject m_BuilderPrefab = null;
    [SerializeField] private GameObject m_SoldierPrefab = null;

    [Header("Misc")]
    [SerializeField] private GameObject m_FlagPrefab = null;

    public ObjectPool castlePool = null;
    public ObjectPool barracksPool = null;
    public ObjectPool builderPool = null;
    public ObjectPool soldierPool = null;
    public ObjectPool flagPool = null;

    private void Awake()
    {
        castlePool = new ObjectPool(5, m_CastlePrefab, new GameObject("CastlePool").transform);
        barracksPool = new ObjectPool(5, m_BarracksPrefab, new GameObject("BarracksPool").transform);
        builderPool = new ObjectPool(5, m_BuilderPrefab, new GameObject("BuilderPool").transform);
        soldierPool = new ObjectPool(5, m_SoldierPrefab, new GameObject("SoliderPool").transform);
        flagPool = new ObjectPool(1, m_FlagPrefab, new GameObject("FlagPool").transform);
    }

    public GameObject GetPooledStructure(StructureType type, bool rent)
    {
        return type switch
        {
            StructureType.Castle => castlePool.Rent(rent),
            StructureType.Barracks => barracksPool.Rent(rent),
            StructureType.None => null,
            _ => null
        };
    }
}
