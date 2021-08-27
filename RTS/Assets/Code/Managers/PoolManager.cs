using Code.Framework.Enums;
using Code.Framework.ObjectPool;
using Code.Logger;
using UnityEngine;

namespace Code.Managers
{
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

        public ObjectPool castlePool = null;
        public ObjectPool barracksPool = null;
        public ObjectPool builderPool = null;
        public ObjectPool soldierPool = null;

        private void Awake()
        {
            castlePool = new ObjectPool(5, m_CastlePrefab, new GameObject("CastlePool").transform);
            barracksPool = new ObjectPool(5, m_BarracksPrefab, new GameObject("BarracksPool").transform);
            builderPool = new ObjectPool(5, m_BuilderPrefab, new GameObject("BuilderPool").transform);
            soldierPool = new ObjectPool(5, m_SoldierPrefab, new GameObject("SoliderPool").transform);
        }

        public GameObject GetPooledStructure(StructureType type, bool rent)
        {
            return type switch
            {
                StructureType.Castle => castlePool.Rent(rent),
                StructureType.Barracks => barracksPool.Rent(rent),
                _ => null
            };
        }

        public GameObject GetPooledUnit(UnitType type, bool rent)
        {
            return type switch
            {
                UnitType.Builder => builderPool.Rent(rent),
                UnitType.Solider => soldierPool.Rent(rent),
                _ => null
            };
        }
    }
}
