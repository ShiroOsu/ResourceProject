using UnityEditor;
using UnityEngine;

namespace Code.Interfaces.Events.Editor
{
    [CustomEditor(typeof(MouseOverEvent))]
    public class MouseOverEventEditor : UnityEditor.Editor
    {
        private SerializedProperty m_InGameTextArea;
        private SerializedProperty m_TextMeshPro;

        public override void OnInspectorGUI()
        {
            var m_MouseOverEvent = (MouseOverEvent)target;

            m_InGameTextArea = serializedObject.FindProperty("inGameTextArea");
            m_TextMeshPro = serializedObject.FindProperty("inGameTextToolTip");
        
            EditorGUILayout.PropertyField(m_InGameTextArea);
            EditorGUILayout.PropertyField(m_TextMeshPro);
            EditorGUILayout.Space();
            serializedObject.ApplyModifiedProperties();
        }
    }
}