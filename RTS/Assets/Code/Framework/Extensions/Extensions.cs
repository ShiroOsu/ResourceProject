using System.Linq;
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
        
        public static void SetUpCreateTimer(GameObject createTimer, string inactiveGameObject)
        {
            createTimer.TryGetComponent<RectTransform>(out var rt);
            var UIObject = Extensions.FindInactiveObject(inactiveGameObject);
            rt.SetParent(UIObject.transform);
            rt.localScale = Vector3.one;
            rt.anchoredPosition3D = UIObject.GetComponent<RectTransform>().anchoredPosition3D;
            rt.localRotation = Quaternion.identity;
        }
    }
}