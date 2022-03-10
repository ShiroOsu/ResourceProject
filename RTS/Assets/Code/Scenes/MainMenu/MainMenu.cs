using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scenes.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}