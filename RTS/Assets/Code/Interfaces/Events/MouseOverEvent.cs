using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Interfaces.Events
{
    public class MouseOverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject inGameTextArea;
        public TextMeshPro inGameTextToolTip;
        public string TextArea { private get; set; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            inGameTextToolTip.SetText(TextArea);
            inGameTextArea.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            inGameTextArea.SetActive(false);
        }
    }
}