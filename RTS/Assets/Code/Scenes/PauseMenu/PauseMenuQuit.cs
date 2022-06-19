using Code.SaveSystem.Data;
using Code.Tools.HelperClasses;
using UnityEditor;
using UnityEngine;
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
            if (quitPanel.activeInHierarchy && KeyCode.Escape.WasKeyPressed())
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
            // Add a quick load option? When application quits it saves
            // to a separate file which can be loaded with a continue button? 
            // TODO: What if unexpected shutdown
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

        private void OnDestroy()
        {
            SaveData.Instance.OnDestroy();
        }
    }
}