using System;
using Code.HelperClasses;
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
        public GameState GetCurrentGameState { get; private set; }

        private PauseControls m_PauseControls;

        private void Awake()
        {
            m_PauseControls = new PauseControls();
            m_PauseControls.Keyboard.SetCallbacks(this);
            GetCurrentGameState = GameState.Running;
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
            if (state == GetCurrentGameState) return;
            
            GetCurrentGameState = state;
            GameStateHandler?.Invoke(state);
        }

        public void OnESCPause(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                var newGameState = GetCurrentGameState == GameState.Running ? GameState.Paused : GameState.Running;
                SetState(newGameState);    
            }
        }
    }
}
