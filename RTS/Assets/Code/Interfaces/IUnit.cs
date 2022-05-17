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
        public UnitData GetUnitData(); // Temp
        public GameObject GetUnitImage(); // Temp
        public GameObject GetUnitObject();

        public void Move(Vector3 destination);
        public bool IsUnitMoving();
    }
}