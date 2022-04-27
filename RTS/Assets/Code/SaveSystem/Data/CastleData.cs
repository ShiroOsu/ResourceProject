using System;
using Code.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

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
            var newObj = Object.Instantiate(prefab);
            newObj.transform.position = position;
            newObj.transform.rotation = rotation;
            flagPosition = flagPos;
        }
    }
}