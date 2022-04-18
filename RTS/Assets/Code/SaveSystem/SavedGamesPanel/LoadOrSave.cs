using System;
using Code.Debugging;
using Code.HelperClasses;
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

#pragma warning disable CS0108, CS0114
        public UnityEngine.Camera camera;
#pragma warning restore CS0108, CS0114
        
        private GameObject m_SaveImage;
        private Sprite m_Sprite;

        // Screenshot
        private readonly int m_Width = Screen.width;
        private readonly int m_Height = Screen.height;
        private RenderTexture m_RenderTexture;
        private Texture2D m_Texture2D;
        private Rect m_Rect;
        public bool CanTakeScreenShot { get; set; }

        private void Awake()
        {
            BindButtons();   
            m_Rect = new Rect(0, 0, m_Width, m_Height);
        }

        private void Start()
        {
            if (SaveOrLoadManager.Instance.GetCurrentSaveOrLoadState == SaveOrLoadState.Load)
            {
                LoadImagesFromSaveFiles();   
            }
        }

        private void ButtonPressed(Image saveImage, int saveIndex)
        {
            if (SaveOrLoadManager.Instance.GetCurrentSaveOrLoadState == SaveOrLoadState.Save && saveImage.sprite == null)
            {
                saveImage.sprite = m_Sprite;
                saveImage.SetImageAlpha(1f);
                Save(saveIndex, Extensions.ConvertImageToByteArray(saveImage));
            }
            else if (SaveOrLoadManager.Instance.GetCurrentSaveOrLoadState == SaveOrLoadState.Save && saveImage.sprite != null)
            {
                OverrideSave(saveImage, saveIndex, Extensions.ConvertImageToByteArray(saveImage));
            }
            else if (SaveOrLoadManager.Instance.GetCurrentSaveOrLoadState == SaveOrLoadState.Load)
            {
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

        private void OverrideSave(Image image, int saveIndex, byte[] imageInBytes)
        {
            Debug.Log("Override Save: " + saveIndex);
            
            image.sprite = m_Sprite;
            image.SetImageAlpha(1f);
            Save(saveIndex, imageInBytes);
        }

        
        private void ScreenShot(UnityEngine.Camera _)
        {
            if (!CanTakeScreenShot) return;
            
            Log.Print("LoadOrSave.cs", "ScreenShot");
            SetupForScreenShot();
            
            camera.targetTexture = m_RenderTexture;
            m_Texture2D.ReadPixels(m_Rect, 0, 0);
            m_Texture2D.Apply();

            camera.targetTexture = null;
            
            var sprite = Sprite.Create(m_Texture2D, m_Rect, Vector2.zero);
            m_Sprite = sprite;
            CanTakeScreenShot = false;
        }

        private void SetupForScreenShot()
        {
            m_RenderTexture = new RenderTexture(m_Width, m_Height, 24);
            m_Texture2D = new Texture2D(m_Width, m_Height, TextureFormat.RGBA32, false);
        }

        public void BindOnPostRender()
        {
            UnityEngine.Camera.onPostRender += ScreenShot;
        }

        private void OnDestroy()
        {
            UnityEngine.Camera.onPostRender -= ScreenShot;
        }

        private void BindButtons()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                var index = i;
                buttons[index].button.onClick.AddListener(() => ButtonPressed(buttons[index].saveImage, buttons[index].index));
            }
        }

        private void LoadImagesFromSaveFiles()
        {
            for (var i = 0; i < buttons.Length; i++)
            {
                var saveFileImage = SaveManager.Instance.LoadImage(i);

                if (saveFileImage != null)
                {
                    buttons[i].saveImage.sprite = Sprite.Create(saveFileImage, m_Rect, Vector2.zero);
                    buttons[i].saveImage.SetImageAlpha(1f);
                }
            }
        }
    }
}