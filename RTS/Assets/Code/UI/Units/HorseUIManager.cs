using Code.Tools.Enums;
using Code.Units;
using UnityEngine;

namespace Code.UI.Units
{
    public class HorseUIManager : UnitUIManager
    {
        public override UnitType Type => UnitType.Horse;
        
        public override void EnableMainUI(bool active, GameObject unit, UnitType type, GameObject image, UnitData data)
        {
            UIManager.Instance.SetUnitStatsInfo(data, active);
            image.SetActive(active);
        }

        protected override void BindButtons()
        {
            throw new System.NotImplementedException();
        }
    }
}
