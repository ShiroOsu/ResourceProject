using Code.HelperClasses;
using Code.SaveSystem.Data;
using UnityEngine;

namespace Code.Managers
{
    public class LoadManager : Singleton<LoadManager>
    {
        [SerializeField] private GameObject castlePrefab;
        [SerializeField] private GameObject barracksPrefab;
        [SerializeField] private GameObject builderPrefab;
        [SerializeField] private GameObject soldierPrefab;
        [SerializeField] private GameObject horsePrefab;
        private SaveData m_Data;
        
        public void InstantiateLoadedData(SaveData data)
        {
            m_Data = data;
            
            data.builderData.InstantiateUnitsInList(builderPrefab);
            data.soldierData.InstantiateUnitsInList(soldierPrefab);
            data.horseData.InstantiateUnitsInList(horsePrefab);
            
            data.castleData.InstantiateStructuresInList(castlePrefab);
            data.barracksData.InstantiateStructuresInList(barracksPrefab);
        }
    }
}