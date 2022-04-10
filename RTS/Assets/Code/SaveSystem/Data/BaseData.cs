using System;
using Code.Managers;

namespace Code.SaveSystem.Data
{
    [Serializable]
    public abstract class BaseData
    {
        public Guid dataID;

        protected BaseData(Guid id)
        {
            dataID = id;
        }

        public virtual void Save(UnityEngine.GameObject gameObject)
        {
            
        }
    }
}