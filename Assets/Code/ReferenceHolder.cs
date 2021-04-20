using UnityEngine;

public class ReferenceHolder : MonoBehaviour
{
    private static ReferenceHolder s_Instance = null;

    public static ReferenceHolder Instance => s_Instance ??= FindObjectOfType<ReferenceHolder>();

    [Header("Pool Prefabs")]
    [SerializeField] private GameObject m_BuilderPrefab = null;
    [SerializeField] private GameObject m_SoldierPrefab = null;
    [SerializeField] private GameObject m_FlagPrefab = null;

    public ObjectPool builderPool = null;
    public ObjectPool soldierPool = null;
    public ObjectPool flagPool = null;

    [Header("Builder")]
    public GameObject BuilderImage = null;
    public GameObject BuilderUI = null;

    [Header("Castle")]
    public GameObject CastleImage = null;
    public GameObject CastleUI = null;
    public GameObject CastleInfo = null;

    [Header("?")]
    public GameObject MouseControls = null;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        builderPool = new ObjectPool(5, m_BuilderPrefab, new GameObject("BuilderPool").transform);
        soldierPool = new ObjectPool(5, m_SoldierPrefab, new GameObject("SoliderPool").transform);
        flagPool = new ObjectPool(1, m_FlagPrefab, new GameObject("FlagPool").transform);
    }

}
