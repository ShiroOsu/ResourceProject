using System;
using Code.HelperClasses;
using Code.SaveSystem.SavedGamesPanel;
using UnityEngine.InputSystem;

namespace Code.Managers
{
    public enum GameState
    {
        MainMenu,
        Running,
        Paused,
    }

    public class GameManager : Singleton<GameManager>, PauseControls.IKeyboardActions
    {
        public event Action <GameState>GameStateHandler;
        public GameState GetCurrentGameState { get; private set; }

        private PauseControls m_PauseControls;
        private LoadOrSave m_LoadOrSave;

        private void Awake()
        {
            m_PauseControls = new PauseControls();
            m_PauseControls.Keyboard.SetCallbacks(this);
            GetCurrentGameState = GameState.MainMenu;

            m_LoadOrSave = Extensions.GetComponentInScene<LoadOrSave>("SavedGamesPanel");
        }

        public void BindLoadOrSaveCamera()
        {
            m_LoadOrSave.Cam = UnityEngine.Camera.current;
            m_LoadOrSave.BindOnPostRender();
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
            if (context.started && Extensions.IsGameInRunState()) m_LoadOrSave.CanTakeScreenShot = true;
            
            if (context.performed)
            {
                var newGameState = GetCurrentGameState == GameState.Running ? GameState.Paused : GameState.Running;
                SetState(newGameState);    
            }
        }

        public void ForceGameState(GameState state)
        {
            GetCurrentGameState = state;
            GameStateHandler?.Invoke(state);
        }
    }
}
