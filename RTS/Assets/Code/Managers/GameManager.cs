using System;
using System.Collections;
using Code.Debugging;
using Code.HelperClasses;
using Code.SaveSystem.Data;
using Code.SaveSystem.SavedGamesPanel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        [SerializeField] private string mainSceneName;
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
            SaveManager.Instance.SceneLoader += SceneLoader;
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

        private void SceneLoader(SaveData data)
        {
            UISceneManager.Instance.SetUIObjectActive("MainMenu", false);
            UISceneManager.Instance.SetUIObjectActive("SavedGamesPanel", false);
            UISceneManager.Instance.SetUIObjectActive("LoadingSceneGameObject");
            StartCoroutine(LoadGameSceneAsync(mainSceneName, data));
        }

        private IEnumerator LoadGameSceneAsync(string sceneName, SaveData data)
        {
            var op = SceneManager.LoadSceneAsync(sceneName);

            while (!op.isDone)
            {
                var value = Mathf.Round(Mathf.Clamp01(op.progress / 0.9f) * 100f);
                Log.Print("GameManager.cs", "Load Scene progress: " + value + "%");
                yield return null;
            }

            PoolManager.Instance.CreatePools();
            
            if (data is not null)
            {
                load(data);
                Log.Print("GameManager.cs", "Instantiate data done");
            }
            
            GameScene();
        }

        private async void load(SaveData data)
        {
            await LoadManager.Instance.StartInstantiateData(data);
        }
        
        private void GameScene()
        {
            UISceneManager.Instance.SetUIObjectActive("LoadingSceneGameObject", false);
            SetState(GameState.Running);
        }

        public void StartGame()
        {
            SceneLoader(null);
        }
    }
}