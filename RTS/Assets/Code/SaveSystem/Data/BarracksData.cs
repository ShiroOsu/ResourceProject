using System;
using UnityEngine;

namespace Code.SaveSystem.Data
{
    [System.Serializable]
    public class BarracksData : BaseData
    {
        public Vector3 position;
        public Quaternion rotation;
            
        // Flag
        public Vector3 flagPosition;

        public BarracksData(Guid id) : base(id) { }
    }
}