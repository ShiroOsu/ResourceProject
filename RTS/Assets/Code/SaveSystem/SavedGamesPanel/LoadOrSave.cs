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

        private GameObject m_SaveImage;
        private Sprite m_Sprite;
        public UnityEngine.Camera Cam { get; set; }

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

            // If m_Rect is not set before LoadImagesFromSaveFiles is called the image wont show.
            m_Rect = new Rect(0, 0, m_Width, m_Height);
        }

        private void Start()
        {
            LoadImagesFromSaveFiles();
        }

        private void ButtonPressed(Image saveImage, int saveIndex)
        {
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

        private void ScreenShot(UnityEngine.Camera _)
        {
            // TODO: Take screenshot before opening save panel.
            
            if (!CanTakeScreenShot) return;

            Log.Print("LoadOrSave.cs", "ScreenShot");
            SetupForScreenShot();

            if (!Cam)
            {
                Cam = UnityEngine.Camera.current;
            }

            Cam.targetTexture = m_RenderTexture;
            m_Texture2D.ReadPixels(m_Rect, 0, 0);
            m_Texture2D.Apply();

            Cam.targetTexture = null;
            
            var sprite = Sprite.Create(m_Texture2D, m_Rect, Vector2.zero);
            m_Sprite = sprite;
            CanTakeScreenShot = false;
        }

        private void SetupForScreenShot()
        {
            m_RenderTexture = new RenderTexture(m_Width, m_Height, 24);
            m_Texture2D = new Texture2D(m_Width, m_Height, TextureFormat.RGBA32, false);
            
            // When taking the first screenshot m_Rect has the size of (0, 0),
            // (why?) which will give a invalid AABB inAABB error. 
            if (m_Rect.size == Vector2.zero)
            {
                m_Rect.size = new Vector2(m_Width, m_Height);
            }
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
                    buttons[i].saveImage.sprite = Sprite.Create(saveFileImage, m_Rect, Vector2.zero);
                    buttons[i].saveImage.SetImageAlpha(1f);
                }
            }
        }
    }
}