using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Pool Prefabs")]
    [SerializeField] private GameObject m_BuilderPrefab = null;
    [SerializeField] private GameObject m_SoldierPrefab = null;
    [SerializeField] private GameObject m_FlagPrefab = null;

    public ObjectPool builderPool = null;
    public ObjectPool soldierPool = null;
    public ObjectPool flagPool = null;


    private void Awake()
    {
        DontDestroyOnLoad(this);

        builderPool = new ObjectPool(5, m_BuilderPrefab, new GameObject("BuilderPool").transform);
        soldierPool = new ObjectPool(5, m_SoldierPrefab, new GameObject("SoliderPool").transform);
        flagPool = new ObjectPool(5, m_FlagPrefab, new GameObject("FlagPool").transform);
    }
}