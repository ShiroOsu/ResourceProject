using System;
using Code.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scenes.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject savedGamesPanel;
        private GameObject m_MainMenu;

        private void Awake()
        {
            m_MainMenu = gameObject;
        }

        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void SavedGames()
        {
            m_MainMenu.SetActive(false);
            savedGamesPanel.SetActive(true);
        }
    }
}