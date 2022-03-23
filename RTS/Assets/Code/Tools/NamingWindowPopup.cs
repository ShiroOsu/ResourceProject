#if UNITY_EDITOR
using System;
using Code.Debugging;
using UnityEditor;
using UnityEngine;

namespace Code.Tools
{
    public class NamingWindowPopup : EditorWindow
    {
        public static event Action SetName;
        private string m_Name;
        
        public static void Init()
        {
            var window = GetWindow(typeof(NamingWindowPopup));
            window.titleContent = new GUIContent("Set Name");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Set Name");
            m_Name = EditorGUILayout.TextField("Name: ", m_Name);

            if (GUILayout.Button("Set Name"))
            {
                if (m_Name != string.Empty)
                {
                    m_Name = m_Name.Replace(' ', '_');
                    CreateSerializationSurrogate.CreateSerializationSurrogate.Name = m_Name;
                    SetName?.Invoke();
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