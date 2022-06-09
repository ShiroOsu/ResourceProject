using Code.Managers;
using Code.UI;
using UnityEngine;

namespace Code.Scenes.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        private GameObject m_SavedGamesPanel;
        private GameObject m_MainMenu;
        private AsyncOperation m_AsyncOperation;

        private void Awake()
        {
            m_MainMenu = gameObject;
            m_SavedGamesPanel = UISceneManager.Instance.GetUISceneObject("SavedGamesPanel");
        }

        // Start Game
        public void LoadMainScene()
        {
            GameManager.Instance.StartGame();
        }

        public void SavedGames()
        {
            m_MainMenu.SetActive(false);
            m_SavedGamesPanel.SetActive(true);
        }
    }
}