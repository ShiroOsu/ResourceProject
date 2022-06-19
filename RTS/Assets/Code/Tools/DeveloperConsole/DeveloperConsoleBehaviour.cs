using Code.Managers;
using Code.Tools.HelperClasses;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Tools.DeveloperConsole
{
    public class DeveloperConsoleBehaviour : Singleton<DeveloperConsoleBehaviour>
    {
        // Inspector
        [SerializeField] private string prefix = "";
        public ConsoleCommand[] commands;

        [Header("UI")] 
        [SerializeField] private GameObject uiCanvas;
        [SerializeField] private TMP_InputField inputField;

        private DeveloperConsole m_DeveloperConsole;
        private DataManager m_DataManager;

        private void Awake()
        {
            m_DeveloperConsole = new(prefix, commands);
        }

        public void Toggle(InputAction.CallbackContext context)
        {
            if (!context.action.triggered)
                return;

            if (GameManager.Instance.GetCurrentGameState == GameState.MainMenu)
                return;

            if (!m_DataManager) { m_DataManager = DataManager.Instance; }

            if (uiCanvas.activeSelf)
            {
                DisableMouseControls();
                uiCanvas.SetActive(false);
            }
            else
            {
                DisableMouseControls(false);
                uiCanvas.SetActive(true);
                inputField.ActivateInputField();
            }
        }

        public void ProcessCommand(string inputValue)
        {
            m_DeveloperConsole.ProcessCommand(inputValue);
            inputField.text = "";
        }

        private void DisableMouseControls(bool enable = true)
        {
            m_DataManager.mouseInputs.enabled = enable;
            m_DataManager.cameraControls.enabled = enable;
        }
    }
}