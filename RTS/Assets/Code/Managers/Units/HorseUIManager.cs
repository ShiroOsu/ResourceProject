using Code.Units;
using UnityEngine;

namespace Code.Managers.Units
{
    public class HorseUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_Image;
        [SerializeField] private UnitData m_Data;
        
        public void EnableMainUI(bool active, GameObject unit)
        {
            UIManager.Instance.SetUnitStatsInfo(m_Data);
            m_Image.SetActive(active);
        }
    }
}
