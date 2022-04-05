using Code.Debugging;
using UnityEngine;

namespace Code.Managers
{
    public class PauseGame : MonoBehaviour
    {
        [SerializeField] private GameObject pauseGameObject;
        [SerializeField] private GameObject mainUI;
    
        private void Awake()
        {
            GameManager.Instance.GameStateHandler += Pause;
        }

        private void Pause(GameState state)
        {
            pauseGameObject.SetActive(state is GameState.Paused);
            mainUI.SetActive(state is not GameState.Paused);
            Time.timeScale = GameState.Paused == state ? 0f : 1f;
            Log.Print("PauseGame.cs", $"GameState: {state}");
        }
    }
}
