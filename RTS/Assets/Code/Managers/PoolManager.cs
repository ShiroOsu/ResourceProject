using Code.Enums;
using Code.HelperClasses;
using UnityEngine;

namespace Code.Managers
{
    public class PoolManager : Singleton<PoolManager>
    {
        [Header("Structures")]
        [SerializeField] private GameObject castlePrefab; 
        [SerializeField] private GameObject barracksPrefab;
        
        [Header("Units")]
        [SerializeField] private GameObject builderPrefab;
        [SerializeField] private GameObject soldierPrefab;
        [SerializeField] private GameObject horseUnitPrefab;

        [Header("Misc")] 
        [SerializeField] private GameObject flagPrefab;

        public ObjectPooler castlePool;
        public ObjectPooler barracksPool;
        public ObjectPooler builderPool;
        public ObjectPooler soldierPool;
        public ObjectPooler horseUnitPool;
        public ObjectPooler flagPool;

        public void CreatePools()
        {
            castlePool = new ObjectPooler(5, castlePrefab, new GameObject("CastlePool").transform);
            barracksPool = new ObjectPooler(5, barracksPrefab, new GameObject("BarracksPool").transform);
            builderPool = new ObjectPooler(5, builderPrefab, new GameObject("BuilderPool").transform);
            soldierPool = new ObjectPooler(5, soldierPrefab, new GameObject("SoliderPool").transform);
            horseUnitPool = new ObjectPooler(5, horseUnitPrefab, new GameObject("HorseUnitPool").transform);
            flagPool = new ObjectPooler(5, flagPrefab, new GameObject("FlagPool").transform);
        }

        public GameObject GetPooledMisc(TextureAssetType type, bool rent)
        {
            return type switch
            {
                _ => null
            };
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
