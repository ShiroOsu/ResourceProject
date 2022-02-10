using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Framework.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}