using System;
using System.Linq;
using Code.Framework.FlowField;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;
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

        public static GameObject FindInactiveObject(string name)
        {
            return UEObject.FindObjectsOfType<GameObject>(true).FirstOrDefault(go => go.name.Equals(name));
        }
        
        #if UNITY_EDITOR
        public static T LoadAsset<T>(string path) where T : ScriptableObject
        {
            return (T)AssetDatabase.LoadAssetAtPath(path, typeof(T));
        }
        #endif
        
        [Serializable]
        public struct ButtonByKey
        {
            public GameObject Object;
            public Button Button;
            public RawImage Image;
            public int Key; // Temp
            // Add ToolTip ?
        }

        // Temp
        public static void InitializeFlowField()
        {
            var flowfieldGO = UEObject.FindObjectsOfType<GameObject>().FirstOrDefault(go => go.name.Equals("FlowField"));
            flowfieldGO.TryGetComponent(out GridController gc);
            gc.InitializeFlowField();
        }
    }
}
