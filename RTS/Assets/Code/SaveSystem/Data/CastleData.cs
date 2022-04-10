using System;
using UnityEngine;

namespace Code.SaveSystem.Data
{
    [Serializable]
    public class CastleData : BaseData
    {
        public Vector3 position;
        public Quaternion rotation;
            
        public Vector3 flagPosition;
        
        public CastleData(Guid uniqueID) : base(uniqueID) { }

        public override void Save(GameObject gameObject)
        {
            position = gameObject.transform.position;
            rotation = gameObject.transform.rotation;
            
        }
    }
}