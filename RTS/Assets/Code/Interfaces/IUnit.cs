using Code.Enums;
using UE = UnityEngine;

namespace Code.Interfaces
{
    public interface IUnit
    {
        public void ShouldSelect(bool select);
        public void ActivateSelectionCircle(bool active);

        public void OnDestroy();

        public TextureAssetType GetUnitType();

        public void Move(UE.Vector3 destination);
        public bool IsUnitMoving();
        
        /// <summary>
        /// Returns current position
        /// </summary>
        /// <returns> Vector3Int </returns>
        public UE.Vector3Int GetPosition();
    }
}