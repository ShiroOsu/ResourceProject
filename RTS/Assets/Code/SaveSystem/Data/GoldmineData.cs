using Code.Interfaces;
using Code.Resources;
using Code.Tools.HelperClasses;
using UnityEngine;

namespace Code.SaveSystem.Data
{
    [System.Serializable]
    public class GoldmineData : IResourceData
    {
        public Vector3 position;
        public Quaternion rotation;
        
        public uint gold;
        public uint workersInMine;
        
        public void Save(GameObject gameObject, uint resourcesLeft, uint workers)
        {
            position = gameObject.transform.position;
            rotation = gameObject.transform.rotation;

            gold = resourcesLeft;
            workersInMine = workers;
        }

        public void Instantiate(GameObject prefab)
        {
            var newObj = Object.Instantiate(prefab);
            newObj.transform.position = position;
            newObj.transform.rotation = rotation;

            var goldmine = newObj.ExGetComponent<Goldmine>();
            Debug.Log("LoadedGold: " + gold);
            goldmine.goldLoadedFromData = gold;
            goldmine.currentWorkersInMine = workersInMine;
        }
    }
}
