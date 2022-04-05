#if UNITY_EDITOR
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Code.Debugging;
using Code.SaveSystem.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
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
        
        /// <summary>
        /// Includes inactive objects
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject FindObject(string name)
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

        public static T GetValue<T>(this SerializationInfo info, string name)
        {
            return (T)info.GetValue(name, typeof(T));
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
    }
}
