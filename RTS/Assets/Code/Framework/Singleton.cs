using System;
using Code.Framework.Logger;
using UnityEngine;

namespace Code.Framework
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance)
                    return instance;

                instance = FindObjectOfType<T>();
                if (!instance)
                {
                    instance = (new GameObject {name = nameof(T), hideFlags = HideFlags.HideAndDontSave}).AddComponent<T>();
                    Log.Message("Singleton.cs", "Created new Singleton of type: " + nameof(T));
                }
                
                return instance;
            }
        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}
