using Code.Interfaces;
using Code.SaveSystem.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Code.Scenes.PauseMenu
{
    public class PauseMenuQuit : MonoBehaviour
    {
        [SerializeField] private GameObject quitPanel;
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;

        public void QuitGame()
        {
            Quit();
            yesButton.onClick.AddListener(Confirmed);
            noButton.onClick.AddListener(Cancel);
        }

        private void Update()
        {
            if (quitPanel.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                quitPanel.SetActive(false);
            }
        }

        private void Quit()
        {
            quitPanel.SetActive(true);
        }

        private void Confirmed()
        {
            SaveData.Instance.OnDestroy();
            
            #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        private void Cancel()
        {
            quitPanel.SetActive(false);
        }
    }
}