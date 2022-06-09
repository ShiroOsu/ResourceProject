using Code.Managers;
using Code.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.SaveSystem.SavedGamesPanel
{
    public class BackButton : MonoBehaviour
    {
        private GameObject m_SavedGamesPanel;
        private GameObject m_MainGameObject;
        private GameObject m_PauseGameObject;
        private GameObject m_MainUI;

        private void Awake()
        {
            m_MainGameObject = UISceneManager.Instance.GetUISceneObject("MainMenu");
            m_SavedGamesPanel = UISceneManager.Instance.GetUISceneObject("SavedGamesPanel");
            //mainGameObject = Extensions.FindObject("MainMenu");
        }

        private void Update()
        {
            if (!m_PauseGameObject)
            {
                m_PauseGameObject = UISceneManager.Instance.GetUISceneObject("PauseScreen");
                //pauseGameObject = Extensions.FindObject("PauseScreen");
            }

            if (!m_MainUI)
            {
                m_MainUI = UISceneManager.Instance.GetUISceneObject("MainUI");
                //m_MainUI = Extensions.FindObject("MainUI");
            }

            if (m_SavedGamesPanel.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                ClosePanel();
            }
        }

        public void ClosePanel()
        {
            m_SavedGamesPanel.SetActive(false);

            if (m_MainUI)
            {
                m_MainUI.SetActive(true);
                GameManager.Instance.ForceGameState(GameState.Running);
            }

            if (m_PauseGameObject)
            {
                m_PauseGameObject.SetActive(false);
            }
            
            if (m_MainGameObject)
            {
                m_MainGameObject.SetActive(true);
            }
        }
    }
}
