using System;
using Code.HelperClasses;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.UI
{
    public class MenuButtons : Singleton<MenuButtons>
    {
        [Serializable]
        private struct ButtonByKey
        {
            public GameObject @object;
            public Button button;
            public RawImage image;
            public int key; // Temp
            // Add ToolTip ?
        }
        
        [SerializeField] private ButtonByKey[] buttonByKey;

        public void BindMenuButton(UnityAction action, int buttonIndex, Texture texture)
        {
            buttonByKey[buttonIndex].button.onClick.AddListener(action);
            buttonByKey[buttonIndex].image.texture = texture;
            buttonByKey[buttonIndex].@object.SetActive(true);
        }

        public void UnBindMenuButtons()
        {
            foreach (var button in buttonByKey)
            {
                button.button.onClick.RemoveAllListeners();
                button.@object.SetActive(false);
                button.image.texture = null;
            }
        }
    }
}
