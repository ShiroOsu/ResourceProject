using UnityEngine;
using UnityEngine.UI;

public class BuilderUIManager : MonoBehaviour
{
    [SerializeField] private GameObject m_BuildingsPage = null;
    [SerializeField] private GameObject m_MainPage = null;
    [SerializeField] private GameObject m_Image = null;
    [SerializeField] private GameObject m_UI = null;

    [SerializeField] private Button m_BuildButton = null;

    private BuilderUnit m_BuilderUnit = null;

    public void EnableMainUI(bool active, GameObject unit)
    {
        m_BuilderUnit = unit.GetComponent<BuilderUnit>();
        m_BuildButton.onClick.AddListener(() => OnButtonBuildPage(active));

        m_MainPage.SetActive(active);
        m_Image.SetActive(active);
        m_UI.SetActive(active);

        if (active is false)
        {
            m_BuildButton.onClick.RemoveListener(() => OnButtonBuildPage(active));
        }
    }

    public void OnButtonBuildPage(bool active)
    {
        m_BuildingsPage.SetActive(active);
        m_MainPage.SetActive(!active);
    }
}