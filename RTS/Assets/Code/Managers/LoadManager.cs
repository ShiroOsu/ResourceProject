using Code.HelperClasses;
using Code.SaveSystem.Data;
using UnityEngine;
using System.Threading.Tasks;
using Code.Debugging;

namespace Code.Managers
{
    public class LoadManager : Singleton<LoadManager>
    {
        [SerializeField] private GameObject castlePrefab;
        [SerializeField] private GameObject barracksPrefab;
        [SerializeField] private GameObject builderPrefab;
        [SerializeField] private GameObject soldierPrefab;
        [SerializeField] private GameObject horsePrefab;

        public async Task StartInstantiateData(SaveData data)
        {
            InstantiateLoadedData(data);
            await Task.Yield();
        }
        
        private void InstantiateLoadedData(SaveData data)
        {
            data.builderData.InstantiateUnitsInList(builderPrefab);
            data.soldierData.InstantiateUnitsInList(soldierPrefab);
            data.horseData.InstantiateUnitsInList(horsePrefab);
            
            data.castleData.InstantiateStructuresInList(castlePrefab);
            data.barracksData.InstantiateStructuresInList(barracksPrefab);
        }
    }
}