using System;
using Code.Framework.Logger;
using UnityEditor;
using UnityEngine;

namespace Code.Framework.Custom
{
    public class AutoCreateWindowPopup : EditorWindow
    {
        public static event Action BeginCreation;
        private string unitNameToSet = "";

        public static void Init()
        {
            var window = GetWindow(typeof(AutoCreateWindowPopup));
            window.titleContent = new GUIContent("Auto Create Unit");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Set name for new unit");
            unitNameToSet = EditorGUILayout.TextField("Name: ", unitNameToSet);
            
            if (GUILayout.Button("Set Name"))
            {
                if (unitNameToSet != string.Empty)
                {
                    unitNameToSet = unitNameToSet.Replace(' ', '_');
                    
                    AutoCreateUnit.m_UnitName = unitNameToSet;
                    BeginCreation?.Invoke();
                    Close();
                }
                else
                {
                    Log.Error("AutoCreateWindowPopup", "You need to set unit name");
                }
            }
        }
    }
}