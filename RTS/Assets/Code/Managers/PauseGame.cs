using Code.Tools.Debugging;
using Code.UI;
using UnityEngine;

namespace Code.Managers
{
    public class PauseGame : MonoBehaviour
    {
        private GameObject m_PauseGameObject;
        private GameObject m_MainUI;
        private DataManager m_DataManager;
    
        private void Awake()
        {
            GameManager.Instance.GameStateHandler += Pause;
            m_DataManager = DataManager.Instance;

            m_PauseGameObject = UISceneManager.Instance.GetUISceneObject("PauseScreen");
            m_MainUI = UISceneManager.Instance.GetUISceneObject("MainUI");
        }

        private void Pause(GameState state)
        {
            m_PauseGameObject.SetActive(state is GameState.Paused);
            m_MainUI.SetActive(state is not GameState.Paused);
            m_DataManager.mouseInputs.enabled = (state is not GameState.Paused);
            Time.timeScale = GameState.Paused == state ? 0f : 1f;
            Log.Print("PauseGame.cs", $"GameState: {state}");
        }
    }
}
