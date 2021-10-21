using UnityEngine;
using UnityEngine.Events;

namespace Code.Framework.UI
{
    public class MenuButtons : MonoBehaviour
    {
        private static MenuButtons s_Instance;
        public static MenuButtons Instance => s_Instance ??= FindObjectOfType<MenuButtons>();

        [SerializeField] private ExtensionFolder.Extensions.ButtonByKey[] m_ButtonByKey;

        public void BindMenuButton(UnityAction action, int buttonIndex, Texture texture)
        {
            m_ButtonByKey[buttonIndex].Button.onClick.AddListener(action);
            m_ButtonByKey[buttonIndex].Image.texture = texture;
            m_ButtonByKey[buttonIndex].Object.SetActive(true);
        }

        public void UnBindMenuButtons()
        {
            foreach (var button in m_ButtonByKey)
            {
                button.Button.onClick.RemoveAllListeners();
                button.Object.SetActive(false);
                button.Image.texture = null;
            }
        }
    }
}
