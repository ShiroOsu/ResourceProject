using Code.Tools.Enums;

namespace Code.Interfaces
{
    public interface IResource
    {
        public void ShouldSelect(bool select);
        public bool AddWorkers(TextureAssetType type);
        public void ReduceResources(uint amount);
    }
}