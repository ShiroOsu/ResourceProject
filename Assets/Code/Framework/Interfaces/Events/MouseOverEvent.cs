using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject inGameTextArea = null;
    public TextMeshPro inGameTextToolTip;
    public string textArea;

    public void OnPointerEnter(PointerEventData eventData)
    {
        inGameTextArea.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inGameTextArea.SetActive(false);
    }

    public void SetToolTipText()
    {
        inGameTextToolTip.SetText(textArea);
    }
}
