using System;
using Code.Debugging;
using Code.Managers;
using Code.SaveSystem.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scenes.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private string mainSceneName;
        private GameObject m_SavedGamesPanel;
        private GameObject m_LoadingScenePanel;
        private GameObject m_MainMenu;
        private AsyncOperation m_AsyncOperation;

        private void Awake()
        {
            m_MainMenu = gameObject;
            m_LoadingScenePanel = UISceneManager.Instance.GetUISceneObject("LoadingSceneGameObject");
            m_SavedGamesPanel = UISceneManager.Instance.GetUISceneObject("SavedGamesPanel");
            
            SaveManager.Instance.SceneLoader += SceneLoader;
        }

        private void SceneLoader(SaveData data)
        {
            m_MainMenu.SetActive(false);
            m_LoadingScenePanel.SetActive(true);
            
            // Start loading progress
            m_AsyncOperation = SceneManager.LoadSceneAsync(mainSceneName);
            //loadingScenePanel.GetComponentInChildren<Canvas>().worldCamera = UnityEngine.Camera.main;
            LoadManager.Instance.InstantiateLoadedData(data);
        }

        private void Update()
        {
            if (m_AsyncOperation == null) return;
            
            var value = Mathf.Clamp01(m_AsyncOperation.progress / 0.9f);
            var valueP = Mathf.Round(value * 100);
            Log.Print("MainMenu.cs", "progress: " + value);
            Log.Print("MainMenu.cs", "progress%: " + valueP + "%");
        }

        // Start Game
        public void LoadMainScene()
        {
            SceneManager.LoadScene(mainSceneName);
            
            GameManager.Instance.ForceGameState(GameState.Running);
        }

        public void SavedGames()
        {
            m_MainMenu.SetActive(false);
            m_SavedGamesPanel.SetActive(true);
        }
    }
}