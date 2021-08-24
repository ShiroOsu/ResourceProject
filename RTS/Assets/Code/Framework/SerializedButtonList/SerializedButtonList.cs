using System;
using System.Collections.Generic;
using Code.Framework.Enums;
using UnityEngine;
using UnityEngine.UI;


namespace Code.Framework.SerializedButtonList
{
    [CreateAssetMenu(fileName = "SerializedButtonList", menuName = "ScriptableObjects/SerializedButtonList")]
    public class SerializedButtonList : ScriptableObject
    {
        [Serializable]
        public struct ButtonType
        {
            public Button m_Button;
            public ButtonName m_Name;
        }

        private readonly Dictionary<ButtonName, Button> m_Dictionary = new Dictionary<ButtonName, Button>();
        public ButtonType[] m_Buttons;

        public Button this[ButtonName m_Name]
        {
            get
            {
                Init();
                return m_Dictionary[m_Name];
            }
        }
        
        private void Init()
        {
            foreach (var button in m_Buttons)
            {
                m_Dictionary[button.m_Name] = button.m_Button;
            }
        }
    }
}
