using Code.Tools.Enums;
using Code.Tools.HelperClasses;
using Code.Tools.ObjectPool;
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
        [SerializeField] private GameObject workerUnitPrefab;

        public ObjectPooler castlePool;
        public ObjectPooler barracksPool;
        public ObjectPooler builderPool;
        public ObjectPooler soldierPool;
        public ObjectPooler horsePool;
        public ObjectPooler workerPool;

        public void CreatePools()
        {
            castlePool = new(5, castlePrefab, new GameObject("CastlePool").transform);
            barracksPool = new(5, barracksPrefab, new GameObject("BarracksPool").transform);
            builderPool = new(5, builderPrefab, new GameObject("BuilderPool").transform);
            soldierPool = new(5, soldierPrefab, new GameObject("SoliderPool").transform);
            horsePool = new(5, horseUnitPrefab, new GameObject("HorseUnitPool").transform);
            workerPool = new(5, workerUnitPrefab, new GameObject("WorkerPool").transform);
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
                TextureAssetType.Horse => horsePool.Rent(rent),
                TextureAssetType.Worker => workerPool.Rent(rent),
                _ => null
            };
        }
    }
}
