using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Code.Framework.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Was left/right mouse button pressed this frame
        /// </summary>
        /// <returns></returns>
        public static bool WasMousePressedThisFrame()
        {
            return (Mouse.current.rightButton.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame);
        }

        public static GameObject FindInactiveObject(string name)
        {
            return UnityEngine.Object.FindObjectsOfType<GameObject>(true).FirstOrDefault(go => go.name.Equals(name));
        }
        
        [Serializable]
        public struct ButtonByKey
        {
            public GameObject Object;
            public Button Button;
            public RawImage Image;
            public int Key; // Temp
            // Add ToolTip
        }
    }
}