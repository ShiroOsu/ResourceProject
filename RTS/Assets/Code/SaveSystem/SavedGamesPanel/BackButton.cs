using Code.HelperClasses;
using UnityEngine;

namespace Code.SaveSystem.SavedGamesPanel
{
    public class BackButton : MonoBehaviour
    {
        public GameObject savedGamesPanel;
        public GameObject mainAndPause;

        private void Awake()
        {
            mainAndPause = Extensions.FindObject("MainMenu");
        }

        public void ClosePanel()
        {
            if (!mainAndPause)
            {
                mainAndPause = Extensions.FindObject("PauseScreen");
            }
            savedGamesPanel.SetActive(false);
            mainAndPause.SetActive(true);
        }
    }
}
