#if UNITY_EDITOR
using System;
using Code.Debugging;
using UnityEditor;
using UnityEngine;

namespace Code.Tools.AutoCreate
{
    public class AutoCreateWindowPopup : EditorWindow
    {
        public static event Action BeginCreation;
        private string m_UnitNameToSet = "";

        public static void Init()
        {
            var window = GetWindow(typeof(AutoCreateWindowPopup));
            window.titleContent = new GUIContent("Auto Create Unit");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Set name for new unit");
            m_UnitNameToSet = EditorGUILayout.TextField("Name: ", m_UnitNameToSet);
            
            if (GUILayout.Button("Set Name"))
            {
                if (m_UnitNameToSet != string.Empty)
                {
                    m_UnitNameToSet = m_UnitNameToSet.Replace(' ', '_');
                    
                    AutoCreateUnit.UnitName = m_UnitNameToSet;
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
#endif