using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Pool Prefabs")]
    public GameObject m_BuilderPrefab = null;
    public GameObject m_SoldierPrefab = null;

    public ObjectPool m_BuilderPool = null;
    public ObjectPool m_SoldierPool = null;


    private void Start()
    {
        DontDestroyOnLoad(this);

        m_BuilderPool = new ObjectPool(5, m_BuilderPrefab, new GameObject("BuilderPool").transform);
        m_SoldierPool = new ObjectPool(5, m_SoldierPrefab, new GameObject("SoliderPool").transform);                            
    }
}