using Code.HelperClasses;
using Code.Interfaces;
using Code.Resources;
using UnityEngine;

namespace Code.SaveSystem.Data
{
    [System.Serializable]
    public class QuarryData : IResourceData
    {
        public Vector3 position;
        public Quaternion rotation;
        
        public uint stone;
        public uint quarryWorkers;
        
        public void Save(GameObject gameObject, uint resourcesLeft, uint workers)
        {
            position = gameObject.transform.position;
            rotation = gameObject.transform.rotation;

            stone = resourcesLeft;
            quarryWorkers = workers;
        }

        public void Instantiate(GameObject prefab)
        {
            var newObj = Object.Instantiate(prefab);
            newObj.transform.position = position;
            newObj.transform.rotation = rotation;

            var quarry = newObj.ExGetComponent<Quarry>();
            quarry.stoneLoadedFromData = stone;
            quarry.currentQuarryWorkers = quarryWorkers;
        }
    }
}
