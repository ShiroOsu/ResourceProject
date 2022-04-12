using System;
using UnityEngine;

namespace Code.SaveSystem.Data
{
    [Serializable]
    public class HorseData : BaseData
    {
        public Vector3 position;
        public Quaternion rotation;

        public HorseData(Guid id) : base(id)
        {
        }
        
        public override void Save(GameObject gameObject)
        {
            position = gameObject.transform.position;
            rotation = gameObject.transform.rotation;
        }
    }
}