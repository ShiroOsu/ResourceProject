using Code.Enums;
using Code.HelperClasses;
using UnityEngine;

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

        public ObjectPooler castlePool = null;
        public ObjectPooler barracksPool = null;
        public ObjectPooler builderPool = null;
        public ObjectPooler soldierPool = null;
        public ObjectPooler horseUnitPool = null;

        private void Awake()
        {
            castlePool = new ObjectPooler(5, castlePrefab, new GameObject("CastlePool").transform);
            barracksPool = new ObjectPooler(5, barracksPrefab, new GameObject("BarracksPool").transform);
            builderPool = new ObjectPooler(5, builderPrefab, new GameObject("BuilderPool").transform);
            soldierPool = new ObjectPooler(5, soldierPrefab, new GameObject("SoliderPool").transform);
            horseUnitPool = new ObjectPooler(5, horseUnitPrefab, new GameObject("HorseUnitPool").transform);
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
                TextureAssetType.Soldier => soldierPool.Rent(rent),
                TextureAssetType.Horse => horseUnitPool.Rent(rent),
                _ => null
            };
        }
    }
}
