using Code.Managers;
using Code.Tools.Debugging;
using Code.Tools.HelperClasses;
using UnityEngine;

namespace Code.Tools
{
    public class Screenshot : Singleton<Screenshot>
    {
        private readonly int m_Width = Screen.width;
        private readonly int m_Height = Screen.height;
        private RenderTexture m_RenderTexture;
        private Texture2D m_Texture2D;
        private UnityEngine.Camera m_MainCamera;
        
        public Rect ScreenRect { get; private set; }
        public Sprite ScreenSprite { get; private set; }
        public bool canTakeScreenshot = true;

        private void Awake()
        {
            ScreenRect = new Rect(0, 0, m_Width, m_Height);
            BindOnPostRender();

            UpdateManager.Instance.OnUpdate += OnUpdate;
        }

        private const float c_ScreenshotCooldown = 60f;
        private float m_Elapsed;
        private void OnUpdate()
        {
            canTakeScreenshot = false;
            
            m_Elapsed += Time.deltaTime;
            
            if (m_Elapsed >= c_ScreenshotCooldown)
            {
                m_Elapsed = 0f;
                canTakeScreenshot = true;
            }
        }

        public void ScreenShot(UnityEngine.Camera cam)
        {
            if (!canTakeScreenshot || !cam.CompareTag("MainCamera")) 
                return;
            
            if (!m_MainCamera && cam.CompareTag("MainCamera"))
            {
                m_MainCamera = cam;
            }
            
            Log.Print("Screenshot.cs", "ScreenShot");
            SetupForScreenShot();

            m_MainCamera.targetTexture = m_RenderTexture;
            m_Texture2D.ReadPixels(ScreenRect, 0, 0);
            m_Texture2D.Apply();

            m_MainCamera.targetTexture = null;
            
            var sprite = Sprite.Create(m_Texture2D, ScreenRect, Vector2.zero);
            ScreenSprite = sprite;
        }
        
        private void SetupForScreenShot()
        {
            m_RenderTexture = new RenderTexture(m_Width, m_Height, 24);
            m_Texture2D = new Texture2D(m_Width, m_Height, TextureFormat.RGBA32, false);
        }
        
        private void BindOnPostRender()
        {
            UnityEngine.Camera.onPostRender += ScreenShot;
        }

        private void OnDestroy()
        {
            UnityEngine.Camera.onPostRender -= ScreenShot;
        }
    }
}