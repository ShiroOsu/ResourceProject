using UnityEngine;

namespace Code.Managers
{
    public class PauseGame : MonoBehaviour
    {
        private void Awake()
        {
            GameManager.Instance.GameStateHandler += EnableAllScripts;
        }

        private void OnDestroy()
        {
            GameManager.Instance.GameStateHandler -= EnableAllScripts;
        }

        private void EnableAllScripts(GameState state)
        {
            Time.timeScale = GameState.Paused == state ? 0f : 1f;
        }
    }
}
