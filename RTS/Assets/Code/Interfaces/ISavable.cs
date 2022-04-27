namespace Code.Interfaces
{
    public interface ISavable
    {
        public void Save();
    }

    public interface IUnitData
    {
        public void Save(UnityEngine.GameObject gameObject);
        public void Instantiate(UnityEngine.GameObject prefab);
    }

    public interface IStructureData
    {
        public void Save(UnityEngine.GameObject gameObject);
        public UnityEngine.Vector3 GetFlagPosition();
        public void Instantiate(UnityEngine.GameObject prefab, UnityEngine.Vector3 flagPos);
    }
}