using System;
using Code.Interfaces;
using Code.Managers;
using UnityEngine;

namespace Code.SaveSystem.Data
{
    [Serializable]
    public class HorseData : IUnitData
    {
        public Vector3 position;
        public Quaternion rotation;

        public void Save(GameObject gameObject)
        {
            position = gameObject.transform.position;
            rotation = gameObject.transform.rotation;
        }

        public void Instantiate(GameObject prefab)
        {
            var newObj = PoolManager.Instance.horseUnitPool.Rent(true);
            newObj.transform.position = position;
            newObj.transform.rotation = rotation;
        }
    }
}