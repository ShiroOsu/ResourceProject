using System;
using Code.HelperClasses;
using Code.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.SaveSystem.SavedGamesPanel
{
    public class BackButton : MonoBehaviour
    {
        public GameObject savedGamesPanel;
        public GameObject mainGameObject;
        public GameObject pauseGameObject;
        private GameObject m_MainUI;

        private void Awake()
        {
            mainGameObject = Extensions.FindObject("MainMenu");
        }

        private void Update()
        {
            if (!pauseGameObject)
            {
                pauseGameObject = Extensions.FindObject("PauseScreen");
            }

            if (!m_MainUI)
            {
                m_MainUI = Extensions.FindObject("MainUI");
            }

            if (savedGamesPanel.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                ClosePanel();
            }
        }

        public void ClosePanel()
        {
            savedGamesPanel.SetActive(false);

            if (m_MainUI)
            {
                m_MainUI.SetActive(true);
                GameManager.Instance.ForceGameState(GameState.Running);
            }

            if (pauseGameObject)
            {
                pauseGameObject.SetActive(false);
            }
            
            if (mainGameObject)
            {
                mainGameObject.SetActive(true);
            }
        }
    }
}
