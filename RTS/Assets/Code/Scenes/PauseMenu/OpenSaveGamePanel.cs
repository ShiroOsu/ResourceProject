using Code.HelperClasses;
using UnityEngine;

namespace Code.Scenes.PauseMenu
{
    public class OpenSaveGamePanel : MonoBehaviour
    {
        private GameObject m_SavedGamesPanel;

        private void Awake()
        {
            m_SavedGamesPanel = Extensions.FindObject("SavedGamesPanel");
        }

        public void OpenPanel()
        {
            m_SavedGamesPanel.SetActive(true);
        }

        private void ClosePanel()
        {
            m_SavedGamesPanel.SetActive(false);
        }
    }
}
