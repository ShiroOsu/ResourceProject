using System;
using UnityEngine;

namespace Code.SaveSystem.Data
{
    [Serializable]
    public class CastleData : BaseData
    {
        public Vector3 position;
        public Quaternion rotation;
            
        // Flag
        public Vector3 flagPosition;
        
        public CastleData(Guid uniqueID) : base(uniqueID) { }
    }
}