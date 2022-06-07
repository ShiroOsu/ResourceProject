namespace Code.Interfaces
{
    public interface IResource
    {
        public void ShouldSelect(bool select);
        public bool AddWorkers(Enums.TextureAssetType type);
        public void ReduceResources(uint amount);
    }
}