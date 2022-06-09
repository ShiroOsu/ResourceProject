using System;
using Code.Tools.HelperClasses;
using UnityEngine;

namespace Code.UI
{
    public class UISceneManager : Singleton<UISceneManager>
    {
        [Serializable]
        private struct UIScene
        {
            public GameObject gameObject;
            public string nameOfObject;
        }

        [SerializeField] private UIScene[] uiScenes;

        public GameObject GetUISceneObject(string nameOfObject)
        {
            foreach (var ui in uiScenes)
            {
                if (ui.nameOfObject != nameOfObject) 
                    continue;
                
                var obj = ui.gameObject;
                    
                if (obj == null)
                {
                    obj = Extensions.FindObject(nameOfObject);
                }

                return obj;
            }

            return null;
        }
        public void SetUIObjectActive(string nameOfObject, bool active = true)
        {
            foreach (var ui in uiScenes)
            {
                if (ui.nameOfObject != nameOfObject) 
                    continue;
                
                var obj = ui.gameObject;
                    
                if (obj == null)
                {
                    obj = Extensions.FindObject(nameOfObject);
                }

                obj.SetActive(active);
            }
        }
    }
}