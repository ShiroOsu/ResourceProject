#if UNITY_EDITOR
using System;
using Code.Tools.Debugging;
using UnityEditor;
using UnityEngine;

namespace Code.Tools
{
    public class NamingWindowPopup : EditorWindow
    {
        public static event Action<string> SetName;
        private string m_Name;
        private string m_NameToSet;
        
        public static void Init()
        {
            var window = GetWindow(typeof(NamingWindowPopup));
            window.titleContent = new GUIContent("Set Name");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Naming Window");
            m_Name = EditorGUILayout.TextField("Name: ", m_Name);

            if (GUILayout.Button("Set Name"))
            {
                if (m_Name != string.Empty)
                {
                    m_Name = m_Name.Replace(' ', '_');
                    m_NameToSet = m_Name;
                    SetName?.Invoke(m_NameToSet);
                    Close();
                }
                else
                {
                    Log.Error("namingWindowPopup", "Name can't be empty.");
                }
            }
        }
    }
}
#endif