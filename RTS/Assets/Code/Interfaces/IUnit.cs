using Code.Enums;
using Code.Units;
using UnityEngine;

namespace Code.Interfaces
{
    public interface IUnit
    {
        public void ShouldSelect(bool select);
        public void ActivateSelectionCircle(bool active);

        public void OnDestroy();

        public UnitType GetUnitType();
        public TextureAssetType GetUnitTexture();
        public UnitData GetUnitData();
        public GameObject GetUnitImage();

        public void Move(Vector3 destination);
        public bool IsUnitMoving();
        
        /// <summary>
        /// Returns current position
        /// </summary>
        /// <returns> Vector3Int </returns>
        public Vector3Int GetPosition();
    }
}