using Code.Debugging;
using Code.HelperClasses;
using Code.Managers;
using Code.Tools;
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
        private Sprite m_Sprite;

        private void Awake()
        {
            BindButtons();
        }

        private void Start()
        {
            LoadImagesFromSaveFiles();
        }

        private void ButtonPressed(Image saveImage, int saveIndex)
        {
            m_Sprite = Screenshot.Instance.ScreenSprite;
            
            if (SaveOrLoadManager.Instance.GetCurrentSaveOrLoadState == SaveOrLoadState.Save &&
                saveImage.sprite == null)
            {
                saveImage.sprite = m_Sprite;
                saveImage.SetImageAlpha(1f);
                Save(saveIndex, Extensions.ConvertImageToByteArray(saveImage));
            }
            else if (SaveOrLoadManager.Instance.GetCurrentSaveOrLoadState == SaveOrLoadState.Save &&
                     saveImage.sprite != null)
            {
                saveImage.sprite = m_Sprite;
                saveImage.SetImageAlpha(1f);
                OverrideSave(saveIndex, Extensions.ConvertImageToByteArray(saveImage));
            }
            else if (SaveOrLoadManager.Instance.GetCurrentSaveOrLoadState == SaveOrLoadState.Load)
            {
                if (saveImage.sprite == null)
                {
                    Log.Error("LoadOrSave.cs", $"There is no saved data on file {saveIndex}!"); 
                    return;
                }
                
                Load(saveIndex);
            }
        }

        private void Load(int saveIndex)
        {
            SaveManager.Instance.Load(saveIndex);
        }

        private void Save(int saveIndex, byte[] imageInBytes)
        {
            SaveManager.Instance.Save(saveIndex, imageInBytes);
        }

        private void OverrideSave(int saveIndex, byte[] imageInBytes)
        {
            Save(saveIndex, imageInBytes);
            Log.Print("LoadOrSave.cs", $"Override Savefile{saveIndex}");
        }

        private void BindButtons()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                var index = i;
                buttons[index].button.onClick
                    .AddListener(() => ButtonPressed(buttons[index].saveImage, buttons[index].index));
            }
        }

        private void LoadImagesFromSaveFiles()
        {
            for (var i = 0; i < buttons.Length; i++)
            {
                var saveFileImage = SaveManager.Instance.LoadImage(i);

                if (saveFileImage != null)
                {
                    buttons[i].saveImage.sprite = Sprite.Create(saveFileImage, Screenshot.Instance.ScreenRect, Vector2.zero);
                    buttons[i].saveImage.SetImageAlpha(1f);
                }
            }
        }
    }
}