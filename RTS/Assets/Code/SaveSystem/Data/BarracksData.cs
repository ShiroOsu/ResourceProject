using System;
using UnityEngine;

namespace Code.SaveSystem.Data
{
    [Serializable]
    public class BarracksData : BaseData
    {
        public Vector3 position;
        public Quaternion rotation;
            
        public Vector3 flagPosition;
        
        public BarracksData(Guid uniqueID) : base(uniqueID) { }

        public override void Save(GameObject gameObject)
        {
            position = gameObject.transform.position;
            rotation = gameObject.transform.rotation;
        }
    }
}