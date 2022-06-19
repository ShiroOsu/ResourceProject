#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Code.Tools
{
    public class IsCompiling : EditorWindow
    {
        public static void Init(string commandName)
        {
            EditorPrefs.SetString("command", commandName);
            var window = GetWindowWithRect(typeof(IsCompiling), new Rect(0, 0, 300, 300));
            window.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Compiling:", EditorApplication.isCompiling ? "Yes" : "No");
            Repaint();

            if (!EditorApplication.isCompiling)
            {
                CreateDeveloperCommand.CreateDeveloperCommand.CreateScriptableObject(EditorPrefs.GetString("command"));
                EditorPrefs.DeleteKey("command");
                Close();    
            }
        }
    }
}
#endif