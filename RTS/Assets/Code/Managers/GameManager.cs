using System;
using Code.HelperClasses;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Managers
{
    public enum GameState
    {
        Running,
        Paused,
    }
    
    public class GameManager : Singleton<GameManager>, PauseControls.IKeyboardActions
    {
        public event Action <GameState>GameStateHandler;
        private GameState m_CurrentGameState;
        private PauseControls m_PauseControls;

        private void Awake()
        {
            m_PauseControls = new PauseControls();
            m_PauseControls.Keyboard.SetCallbacks(this);
            m_CurrentGameState = GameState.Running;
        }

        private void OnEnable()
        {
            m_PauseControls.Enable();
        }

        private void OnDisable()
        {
            m_PauseControls.Disable();
        }

        private void SetState(GameState state)
        {
            if (state == m_CurrentGameState) return;
            
            m_CurrentGameState = state;
            GameStateHandler?.Invoke(state);
        }

        private void OnGUI()
        {
            if (m_CurrentGameState == GameState.Paused)
            {
                GUI.Label(new Rect(Screen.width * 0.5f - 50f, Screen.height * 0.5f - 50f, 100f, 100f), "Game Paused");
            }
        }

        public void OnESCPause(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                var newGameState = m_CurrentGameState == GameState.Running ? GameState.Paused : GameState.Running;
                SetState(newGameState);    
            }
        }
    }
}
