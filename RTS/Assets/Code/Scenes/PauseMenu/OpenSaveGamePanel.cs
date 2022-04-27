using Code.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scenes.PauseMenu
{
    public class OpenSaveGamePanel : MonoBehaviour
    {
        private GameObject m_SavedGamesPanel;
        private GameObject m_PausedGamePanel;

        private void Start()
        {
            m_SavedGamesPanel = UISceneManager.Instance.GetUISceneObject("SavedGamesPanel");
            //m_SavedGamesPanel = Extensions.FindObject("SavedGamesPanel");
            m_PausedGamePanel = gameObject;
        }

        private void Update()
        {
            if (m_SavedGamesPanel.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                ClosePanel(false);
            }
        }

        public void OpenPanel()
        {
            m_SavedGamesPanel.SetActive(true);
            m_PausedGamePanel.SetActive(false);
        }

        private void ClosePanel(bool b = true)
        {
            m_SavedGamesPanel.SetActive(false);
            m_PausedGamePanel.SetActive(b);
        }
    }
}
