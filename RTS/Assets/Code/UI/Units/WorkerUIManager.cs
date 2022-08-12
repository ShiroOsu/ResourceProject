using Code.Tools.Enums;
using Code.Units;
using Code.Units.Worker;
using UnityEngine;

namespace Code.UI.Units
{
    public class WorkerUIManager : UnitUIManager
    {
        private WorkerUnit m_WorkerUnit;
        public override UnitType Type => UnitType.Worker;
        public override void EnableMainUI(bool active, GameObject unit, UnitType type, GameObject image, UnitData data)
        {
            m_WorkerUnit = unit.GetComponent<WorkerUnit>();

            UIManager.Instance.SetUnitStatsInfo(data, active);

            if (active)
            {
                BindButtons();
            }
            
            image.SetActive(active);

            if (!active)
            {
                MenuButtons.Instance.UnBindMenuButtons();
            }         
        }

        protected override void BindButtons()
        {
        }
    }
}