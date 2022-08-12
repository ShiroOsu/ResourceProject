using Code.Tools.Enums;
using UnityEngine;

namespace Code.Interfaces
{
    public interface IUnit
    {
        public void ShouldSelect(bool select);
        public void ActivateSelectionCircle(bool active);
        public UnitType GetUnitType();
        public TextureAssetType GetUnitTexture();
        public GameObject GetUnitImage();
        public GameObject GetUnitObject();
        public void Move(Vector3 destination);
        public void StopAgent(bool stop);
    }
}