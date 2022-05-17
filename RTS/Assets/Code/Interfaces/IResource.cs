namespace Code.Interfaces
{
    public interface IResource
    {
        public void ShouldSelect(bool select);
        public bool AddWorkerToMine(Enums.TextureAssetType type);
        public void ReduceResources(uint amount);
    }
}