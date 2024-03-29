using System.Collections.Generic;
using Code.Tools.HelperClasses;
using UnityEngine;

namespace Code.Managers
{
    public class DontDestroyOnLoadManager : Singleton<DontDestroyOnLoadManager>
    {
        [SerializeField] private List<GameObject> dontDestroyOnLoad = new();
        
        private void Awake()
        {
            foreach (var obj in dontDestroyOnLoad)
            {
                if (!obj)
                {
                    continue;
                }
                
                DontDestroyOnLoad(obj);
            }
            
            DontDestroyOnLoad(this);
            
            #if UNITY_EDITOR
                Debug.unityLogger.logEnabled = true;
            #else
                Debug.unityLogger.logEnabled = false;
            #endif
        }
    }
}
