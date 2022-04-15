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
        public UnityEngine.Camera camera;
        private GameObject m_SaveImage;
        private Sprite m_Sprite;

        // if saving from within game or loading from MainMenu
        public bool isSaving;
        
        // Screenshot
        private readonly int m_Width = Screen.width;
        private readonly int m_Height = Screen.height;
        private RenderTexture m_RenderTexture;
        private Texture2D m_Texture2D;
        private Rect m_Rect;
        public bool CanTakeScreenShot { get; set; }

        private void Awake() => BindButtons();

        private void ButtonPressed(Image saveImage, int saveIndex)
        {
            //saveImage.sprite is null
            if (isSaving) // test
            {
                saveImage.sprite = m_Sprite;
                saveImage.SetImageAlpha(1f);
                Save(saveIndex);
            }
            else if (isSaving && saveImage.sprite)
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
            SaveManager.Instance.Load(saveIndex);
        }

        private static void Save(int saveIndex)
        {
            SaveManager.Instance.Save(saveIndex);
        }

        private void OverrideSave(Image image, int saveIndex)
        {
            image.sprite = m_Sprite;
            image.SetImageAlpha(1f);
            Save(saveIndex);
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
            m_Rect = new Rect(0, 0, m_Width, m_Height);
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
    }
}