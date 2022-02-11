using UnityEngine;

namespace Code.Player
{
    public class CursorSettings : MonoBehaviour
    {
        [SerializeField] private Texture2D cursorTexture;
        
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
