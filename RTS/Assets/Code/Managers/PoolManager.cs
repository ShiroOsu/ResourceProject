using Code.Framework;
using Code.Framework.Enums;
using Code.Framework.ObjectPool;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Managers
{
    public class PoolManager : Singleton<PoolManager>
    {
        [Header("Structures")]
        [SerializeField] private GameObject castlePrefab = null; 
        [SerializeField] private GameObject barracksPrefab = null;
        
        [Header("Units")]
        [SerializeField] private GameObject builderPrefab = null;
        [SerializeField] private GameObject soldierPrefab = null;
        [SerializeField] private GameObject horseUnitPrefab = null;

        public ObjectPool castlePool = null;
        public ObjectPool barracksPool = null;
        public ObjectPool builderPool = null;
        public ObjectPool soldierPool = null;
        public ObjectPool horseUnitPool = null;

        private void Awake()
        {
            castlePool = new ObjectPool(5, castlePrefab, new GameObject("CastlePool").transform);
            barracksPool = new ObjectPool(5, barracksPrefab, new GameObject("BarracksPool").transform);
            builderPool = new ObjectPool(5, builderPrefab, new GameObject("BuilderPool").transform);
            soldierPool = new ObjectPool(5, soldierPrefab, new GameObject("SoliderPool").transform);
            horseUnitPool = new ObjectPool(5, horseUnitPrefab, new GameObject("HorseUnitPool").transform);
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

        public GameObject GetPooledUnit(TextureAssetType type, bool rent)
        {
            return type switch
            {
                TextureAssetType.Builder => builderPool.Rent(rent),
                TextureAssetType.Solider => soldierPool.Rent(rent),
                TextureAssetType.Horse => horseUnitPool.Rent(rent),
                _ => null
            };
        }
    }
}
