using System;
using Code.Managers;
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

        private void FixedUpdate()
        {
            if (GameManager.Instance.GetCurrentGameState == GameState.Paused)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
