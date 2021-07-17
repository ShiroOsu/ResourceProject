using UnityEditor;
using UnityEngine;

namespace Code.Framework.Interfaces.Events.Editor
{
    [CustomEditor(typeof(MouseOverEvent))]
    public class MouseOverEventEditor : UnityEditor.Editor
    {
        private SerializedProperty m_TextArea;
        private SerializedProperty m_InGameTextArea;
        private SerializedProperty m_TextMeshPro;

        public override void OnInspectorGUI()
        {
            var m_MouseOverEvent = (MouseOverEvent)target;

            m_TextArea = serializedObject.FindProperty("textArea");
            m_InGameTextArea = serializedObject.FindProperty("inGameTextArea");
            m_TextMeshPro = serializedObject.FindProperty("inGameTextToolTip");
        
            EditorGUILayout.PropertyField(m_InGameTextArea);
            EditorGUILayout.PropertyField(m_TextMeshPro);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(m_TextArea);


            if (GUILayout.Button("Set tool tip text"))
            {
                m_MouseOverEvent.SetToolTipText();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}