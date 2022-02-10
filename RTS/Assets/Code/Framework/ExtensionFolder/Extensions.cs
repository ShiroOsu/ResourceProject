using System;
using System.Collections.Generic;
using System.Linq;
using Code.Framework.FlowField;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UEObject = UnityEngine.Object;

namespace Code.Framework.ExtensionFolder
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

        /// <summary>
        /// Was left/right mouse button released this frame
        /// </summary>
        /// <returns></returns>
        public static bool WasMouseReleasedThisFrame()
        {
            return (Mouse.current.rightButton.wasReleasedThisFrame || Mouse.current.leftButton.wasReleasedThisFrame);
        }

        public static GameObject FindInactiveObject(string name)
        {
            return UEObject.FindObjectsOfType<GameObject>(true).FirstOrDefault(go => go.name.Equals(name));
        }
        
        #if UNITY_EDITOR
        /// <summary>
        /// Load Asset from String Path
        /// </summary>
        /// <typeparam name="T"> is ScriptableObject </typeparam>
        /// <returns></returns>
        public static T LoadAsset<T>(string path) where T : ScriptableObject
        {
            return (T)AssetDatabase.LoadAssetAtPath(path, typeof(T));
        }
        #endif
        
        /// <summary>
        /// This is for buttons in the right of the UI
        /// </summary>
        [Serializable]
        public struct ButtonByKey
        {
            public GameObject @object;
            public Button button;
            public RawImage image;
            public int key; // Temp
            // Add ToolTip ?
        }
    }
}
