using System;

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
    }
}