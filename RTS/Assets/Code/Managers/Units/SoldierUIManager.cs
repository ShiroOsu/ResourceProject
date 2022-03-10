using Code.Managers.UI;
using Code.Units;
using UnityEngine;

namespace Code.Managers.Units
{
    public class SoldierUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject image;
        [SerializeField] private UnitData data;

        public void EnableMainUI(bool active, GameObject unit)
        {
            UIManager.Instance.SetUnitStatsInfo(data);
            image.SetActive(active);
        }
    }
}
