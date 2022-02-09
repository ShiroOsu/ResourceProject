using Code.Framework.Enums;

namespace Code.Framework.Interfaces
{
    public interface IUnit
    {
        public void ShouldSelect(bool select);
        public void ActivateSelectionCircle(bool active);

        public void OnDestroy();

        public TextureAssetType GetUnitType();

        public void Move(UnityEngine.Vector3 destination);
    }
}