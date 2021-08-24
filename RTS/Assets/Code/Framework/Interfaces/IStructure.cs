namespace Code.Framework.Interfaces
{
    public interface IStructure
    {
        public void ShouldSelect(bool select);

        public void Destroy();

        public void Upgrade();
    }
}
