using System.Linq;
using Code.Framework.Enums;
using UnityEngine;
using UnityEngine.InputSystem;

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
            return Object.FindObjectsOfType<GameObject>(true).FirstOrDefault(go => go.name.Equals(name));
        }
    }
}