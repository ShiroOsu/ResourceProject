using System;
using Code.Interfaces;
using Code.Managers;
using Code.Structures;
using UnityEngine;

namespace Code.SaveSystem.Data
{
    [Serializable]
    public class CastleData : IStructureData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 flagPosition;

        public void Save(GameObject gameObject)
        {
            position = gameObject.transform.position;
            rotation = gameObject.transform.rotation;
        }
        
        public Vector3 GetFlagPosition()
        {
            return flagPosition;
        }

        public void Instantiate(GameObject prefab, Vector3 flagPos)
        {
            var newObj = PoolManager.Instance.castlePool.Rent(true);
            newObj.transform.position = position;
            newObj.transform.rotation = rotation;
            
            var flagObj = FlagManager.Instance.InstantiateNewFlag();
            newObj.TryGetComponent<Castle>(out var castle);
            castle.Flag = flagObj;
        }
    }
}