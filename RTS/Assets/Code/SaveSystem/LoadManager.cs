using System.Threading.Tasks;
using Code.SaveSystem.Data;
using Code.Tools.HelperClasses;
using UnityEngine;

namespace Code.SaveSystem
{
    public class LoadManager : Singleton<LoadManager>
    {
        // TODO: List by Enum
        [SerializeField] private GameObject castlePrefab;
        [SerializeField] private GameObject barracksPrefab;
        [SerializeField] private GameObject builderPrefab;
        [SerializeField] private GameObject soldierPrefab;
        [SerializeField] private GameObject horsePrefab;
        [SerializeField] private GameObject goldminePrefab;

        public async Task StartInstantiateData(SaveData data)
        {
            InstantiateLoadedData(data);
            await Task.Yield();
        }
        
        private void InstantiateLoadedData(SaveData data)
        {
            // units
            data.builderData.InstantiateUnitsInList(builderPrefab);
            data.soldierData.InstantiateUnitsInList(soldierPrefab);
            data.horseData.InstantiateUnitsInList(horsePrefab);
            
            // structures
            data.castleData.InstantiateStructuresInList(castlePrefab);
            data.barracksData.InstantiateStructuresInList(barracksPrefab);
            
            // resources
            data.goldminesData.InstantiateResourcesInList(goldminePrefab);
            
            // player
            data.playerResourceData.LoadResources();
        }
    }
}