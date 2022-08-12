using System;
using Code.Interfaces.Events;
using Code.Managers;
using Code.Tools.HelperClasses;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.UI
{
    public class MenuButtons : Singleton<MenuButtons>
    {
        private void Awake()
        {
            UpdateManager.Instance.OnUpdate += OnUpdate;
        }

        [Serializable]
        private struct ButtonByKey
        {
            public GameObject @object;
            public Button button;
            public RawImage image;
            public MouseOverEvent mouseOverEvent;
            public TMP_Text keyBind;
            public KeyCode key;
        }

        [SerializeField] private ButtonByKey[] buttonByKey;

        /// <summary>
        /// Buttons in UI for the corresponding unit/structure.
        /// </summary>
        /// <param name="action"> Action on pressed. </param>
        /// <param name="buttonIndex"> Index of buttons. </param>
        /// <param name="texture"> Texture of button. </param>
        /// <param name="toolTip"> Tooltip text when mouse hover over button. </param>
        /// <param name="key"> Hotkey for the button. </param>
        public void BindMenuButton(UnityAction action, int buttonIndex, Texture texture, string toolTip, KeyCode key = KeyCode.None)
        {
            // W, A, S, D moves the camera
            Assert.IsFalse(key is KeyCode.W or KeyCode.A or KeyCode.S or KeyCode.D);
            
            buttonByKey[buttonIndex].button.onClick.AddListener(action);
            buttonByKey[buttonIndex].image.texture = texture;
            buttonByKey[buttonIndex].mouseOverEvent.TextArea = toolTip;
            buttonByKey[buttonIndex].key = key;
            buttonByKey[buttonIndex].keyBind.SetText(key.ToString());
            buttonByKey[buttonIndex].@object.SetActive(true);
        }

        private void OnUpdate()
        {
            foreach (var key in buttonByKey)
            {
                if (key.key == KeyCode.None) continue;
            
                if (key.key.WasKeyPressed() && key.@object.activeSelf)
                {
                    key.button.onClick.Invoke();
                    return;
                }
            }
        }

        public void UnBindMenuButtons()
        {
            foreach (var button in buttonByKey)
            {
                var byKey = button;

                byKey.button.onClick.RemoveAllListeners();
                byKey.mouseOverEvent.TextArea = string.Empty;
                byKey.image.texture = null;
                byKey.keyBind.SetText(string.Empty);
                byKey.key = KeyCode.None;
                byKey.@object.SetActive(false);
            }
        }
    }
}