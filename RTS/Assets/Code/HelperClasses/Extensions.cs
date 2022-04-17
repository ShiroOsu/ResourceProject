#if UNITY_EDITOR
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Code.Debugging;
using Code.Managers;
using Code.SaveSystem.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UEObject = UnityEngine.Object;

namespace Code.HelperClasses
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

        public static bool IsGameInRunState()
        {
            return GameManager.Instance.GetCurrentGameState == GameState.Running;
        }
        
        public static GameObject FindObject(string name, bool includeInactive = true)
        {
            return UEObject.FindObjectsOfType<GameObject>(includeInactive).FirstOrDefault(go => go.name.Equals(name));
        }

        public static T FindObjectOfTypeAndName<T>(string name, bool includeInactive = true) where T : UEObject
        {
            return UEObject.FindObjectsOfType<T>(includeInactive).FirstOrDefault(go => go.name.Equals(name));
        }

        public static T GetComponentInScene<T>(string name, bool includeInactive = true) where T : Component
        {
            var obj = FindObjectOfTypeAndName<T>(name, includeInactive);

            if (obj.GetComponent<T>() is null)
            {
                Log.Error("Extensions.cs", $"Obj.GetComponent<{typeof(T)}> returned null!");
            }
            
            return obj.GetComponent<T>();
        }
        
        public static byte[] ConvertImageToByteArray(Image image)
        {
            var texture = (Texture2D)image.mainTexture;
            var bytes = texture.EncodeToPNG();
            return bytes;
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

        public static T GetValue<T>(this SerializationInfo info, string name)
        {
            return (T)info.GetValue(name, typeof(T));
        }

        public static void AddValue<T>(this SerializationInfo info, string name)
        {
            info.AddValue(name, typeof(T));
        }

        public static T GetDataByID<T>(this List<T> dataList, Guid id) where T : BaseData
        {
            foreach (var data in dataList)
            {
                if (data.dataID == id)
                {
                    return data;
                }
            }

            return null;
        }

        public static IEnumerator AsCoroutine(this Task task)
        {
            return new WaitUntil(() => task.IsCompleted);
        }
        
        public static Vector3Int Vector3ToVector3Int(this Vector3 v)
        {
            return new Vector3Int((int)v.x, (int)v.y, (int)v.z);
        }

        // Alpha: 0 - 1
        public static void SetImageAlpha(this Image image, float alpha)
        {
            var tempColor = image.color;
            tempColor.a = alpha;
            image.color = tempColor;
        }
    }
}
