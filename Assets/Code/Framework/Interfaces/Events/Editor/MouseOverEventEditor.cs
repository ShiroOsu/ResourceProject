using Code.Framework.Interfaces.Events;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MouseOverEvent))]
public class MouseOverEventEditor : Editor
{
    SerializedProperty m_TextArea;
    SerializedProperty m_InGameTextArea;
    SerializedProperty m_TextMeshPro;

    public override void OnInspectorGUI()
    {
        MouseOverEvent m_MouseOverEvent = (MouseOverEvent)target;

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