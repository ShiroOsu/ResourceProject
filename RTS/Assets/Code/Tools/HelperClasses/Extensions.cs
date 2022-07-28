#if UNITY_EDITOR
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Code.Interfaces;
using Code.Managers;
using Code.SaveSystem;
using Code.Tools.Debugging;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using UEObject = UnityEngine.Object;

namespace Code.Tools.HelperClasses
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

        public static bool WasLeftMousePressed()
        {
            return Mouse.current.leftButton.wasPressedThisFrame;
        }
        
        public static bool WasRightMousePressed()
        {
            return Mouse.current.rightButton.wasPressedThisFrame;
        }
        
        public static bool WasLeftMouseReleased()
        {
            return Mouse.current.leftButton.wasReleasedThisFrame;
        }
        
        public static bool WasRightMouseReleased()
        {
            return Mouse.current.rightButton.wasReleasedThisFrame;
        }

        public static KeyCode GetCurrentKeyPressed()
        {
            foreach (var key in _lookup)
            {
                var keyCode = (KeyCode)key;
                if (keyCode.WasKeyPressed())
                {
                    return keyCode;
                }
            }
            
            Log.Print("Extensions.cs", "Key not found!");
            return KeyCode.None;
        }

        public static bool WasKeyPressed(this KeyCode key)
        {
            return Keyboard.current[_lookup[(int)key]].wasPressedThisFrame;
        }
        
        public static bool WasKeyReleased(this KeyCode key)
        {
            return Keyboard.current[_lookup[(int)key]].wasReleasedThisFrame;
        }

        public static bool IsKeyPressing(this KeyCode key, float threshold = 0f)
        {
            return Keyboard.current[_lookup[(int)key]].IsActuated(threshold);
        }

        private static Key[] _lookup = CreateKeyArray();
        private static Key[] CreateKeyArray()
        {
            var keyCodes = Enum.GetValues(typeof(KeyCode));
            var maxKey = keyCodes.Cast<int>().Max();
            _lookup = new Key[maxKey];

            foreach (KeyCode key in keyCodes)
            {
                var keyStr = key.ToString();
                if (Enum.TryParse<Key>(keyStr, true, out var value))
                {
                    _lookup[(int)key] = value;
                }
            }
            _lookup[(int)KeyCode.Return] = Key.Enter;
            _lookup[(int)KeyCode.KeypadEnter] = Key.NumpadEnter;
            return _lookup;
        }

        /// <summary>
        /// T is Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
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

        public static T LoadData<T>(string filepath)
        {
            return (T) SerializationManager.Load(Application.persistentDataPath + filepath);
        }

        public static T ExGetComponent<T>(this GameObject gameObject)
        {
            var b = gameObject.TryGetComponent(out T component);
            if (!b)
            {
                Log.Error("Extensions.cs", $"{gameObject.name} does not have component of type: {typeof(T)}!");
            }
            return component;
        }
        
        // Convert image to a savable format
        public static byte[] ConvertImageToByteArray(Image image)
        {
            var texture = (Texture2D) image.mainTexture;
            var bytes = texture.EncodeToPNG();
            return bytes;
        }

        public static bool StringEquals(this string str, string strToCompare)
        {
            return str.Equals(strToCompare, StringComparison.OrdinalIgnoreCase);
        }

#if UNITY_EDITOR
        /// <summary>
        /// Load Asset from String Path
        /// </summary>
        /// <typeparam name="T"> is ScriptableObject </typeparam>
        /// <returns></returns>
        public static T LoadAsset<T>(string path) where T : ScriptableObject
        {
            return (T) AssetDatabase.LoadAssetAtPath(path, typeof(T));
        }
#endif

        public static T GetValue<T>(this SerializationInfo info, string name)
        {
            return (T) info.GetValue(name, typeof(T));
        }

        public static void AddValue<T>(this SerializationInfo info, string name)
        {
            info.AddValue(name, typeof(T));
        }

        public static IEnumerator AsCoroutine(this Task task)
        {
            return new WaitUntil(() => task.IsCompleted);
        }

        public static void InstantiateUnitsInList<T>(this List<T> dataList, GameObject prefab) where T : IUnitData
        {
            foreach (var data in dataList)
            {
                data.Instantiate(prefab);
                Log.Print("Extensions.cs", "Instantiate Unit: " + prefab.name);
            }
        }

        public static void InstantiateStructuresInList<T>(this List<T> dataList, GameObject prefab) where T : IStructureData
        {
            foreach (var data in dataList)
            {
                data.Instantiate(prefab, data.GetFlagPosition());
                Log.Print("Extensions.cs", "Instantiate Structure: " + prefab.name);
            }
        }

        public static void InstantiateResourcesInList<T>(this List<T> dataList, GameObject prefab) where T : IResourceData
        {
            foreach (var data in dataList)
            {
                data.Instantiate(prefab);
                Log.Print("Extensions.cs", "Instantiate Resource: " + prefab.name);
            }
        }

        public static T GetComponentTFromObj<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() != null ? gameObject.GetComponent<T>() : null;
        }

        public static Vector3Int Vector3ToVector3Int(this Vector3 v)
        {
            return new Vector3Int((int) v.x, (int) v.y, (int) v.z);
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