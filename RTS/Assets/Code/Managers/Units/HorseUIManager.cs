using UnityEngine;

namespace Code.Managers.Units
{
    public class HorseUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_Image;
        //[SerializeField] private GameObject m_UI;

        public void EnableMainUI(bool active, GameObject unit)
        {
            m_Image.SetActive(active);
            //m_UI.SetActive(active);
        }
    }
}
