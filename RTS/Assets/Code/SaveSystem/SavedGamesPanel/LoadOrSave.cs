using System;
using Code.Debugging;
using Code.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Code.SaveSystem.SavedGamesPanel
{
    public class LoadOrSave : MonoBehaviour
    {
        [System.Serializable]
        private struct SaveButton
        {
            public Image saveImage;
            public Button button;
            public int index;
        }

        [SerializeField] private SaveButton[] buttons;
        private GameObject m_SaveImage;

        // if saving from within game or loading from MainMenu
        public bool m_IsSaving;

        private void Awake() => BindButtons();

        private void ButtonPressed(Image saveImage, int saveIndex)
        {
            //saveImage.sprite is null
            if (m_IsSaving) // test
            {
                AddSaveImage(saveImage);
                Save(saveIndex);
            }
            else if (m_IsSaving && saveImage.sprite)
            {
                OverrideSave(saveImage, saveIndex);
            }
            else
            {
                Load(saveIndex);
            }
        }
        
        private static void Load(int saveIndex)
        {
            Log.Print("LoadOrSave.cs", "Load index: " + saveIndex);
            SaveManager.Instance.Load(saveIndex);
        }

        private static void Save(int saveIndex)
        {
            Log.Print("LoadOrSave.cs", "Save index: " + saveIndex);
            SaveManager.Instance.Save(saveIndex);
        }

        private static void OverrideSave(Image image, int saveIndex)
        {
            AddSaveImage(image);
            Save(saveIndex);
        }

        private static void AddSaveImage(Image imageToSaveTo)
        {
            // Get new image and save to imageToSaveTo
        }

        private void BindButtons()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                var index = i;
                buttons[index].button.onClick.AddListener(() => ButtonPressed(buttons[index].saveImage, buttons[index].index));
            }
        }
    }
}
