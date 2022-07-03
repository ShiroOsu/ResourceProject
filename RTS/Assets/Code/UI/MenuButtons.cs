using System;
using Code.Managers;
using Code.Tools.Debugging;
using Code.Tools.HelperClasses;
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
            public KeyCode key;
        }

        [SerializeField] private ButtonByKey[] buttonByKey;

        public void BindMenuButton(UnityAction action, int buttonIndex, Texture texture, KeyCode key = KeyCode.None)
        {
            Assert.IsFalse(key is KeyCode.W or KeyCode.A or KeyCode.S or KeyCode.D);

            buttonByKey[buttonIndex].button.onClick.AddListener(action);
            buttonByKey[buttonIndex].image.texture = texture;
            buttonByKey[buttonIndex].key = key;
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
                }
            }
        }

        public void UnBindMenuButtons()
        {
            foreach (var button in buttonByKey)
            {
                var byKey = button;

                byKey.button.onClick.RemoveAllListeners();
                byKey.@object.SetActive(false);
                byKey.image.texture = null;
                byKey.key = KeyCode.None;
            }
        }
    }
}