using Code.Enums;
using Code.Units;
using UnityEngine;

namespace Code.Managers.Units
{
    public abstract class UnitUIManager
    {
        protected UnitUIManager(){}

        public abstract UnitType Type { get; }
        
        public abstract void EnableMainUI(bool active, GameObject unit, UnitType type, GameObject image, UnitData data);
        protected abstract void BindButtons();
    }
}
