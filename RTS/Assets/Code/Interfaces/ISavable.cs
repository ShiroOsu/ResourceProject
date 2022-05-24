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

    public interface IResourceData
    {
        public void Save(UnityEngine.GameObject gameObject, uint resourceLeft, uint workers);
        public void Instantiate(UnityEngine.GameObject prefab);
    }

    public interface IPlayerResourceData
    {
        public void Save(int gold, int stone, int wood, int food);
        public void LoadResources();
    }
}