using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Code.Framework.UI
{
    public class MenuButtons : Singleton<MenuButtons>
    {
        [SerializeField] private ExtensionFolder.Extensions.ButtonByKey[] buttonByKey;

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
