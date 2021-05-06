using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilderUIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject m_BuildingsPage = null;
    [SerializeField] private GameObject m_MainPage = null;
    [SerializeField] private GameObject m_Image = null;
    [SerializeField] private GameObject m_UI = null;
    [SerializeField] private Button m_BuildButton = null;

    [Header("Buildings")]
    [SerializeField] private Button m_BarracksButton = null;

    private BuilderUnit m_BuilderUnit = null;

    public void EnableMainUI(bool active, GameObject unit)
    {
        m_BuilderUnit = unit.GetComponent<BuilderUnit>();
        m_BuildButton.onClick.AddListener(() => OnButtonBuildPage(active));

        m_BarracksButton.onClick.AddListener(() => m_BuilderUnit.OnStructureBuildButton(StructureType.Barracks));

        m_MainPage.SetActive(active);
        m_Image.SetActive(active);
        m_UI.SetActive(active);

        if (active is false)
        {
            OnButtonBuildPage(active);

            m_BuildButton.onClick.RemoveAllListeners();
            m_BarracksButton.onClick.RemoveAllListeners();
        }
    }

    public void OnButtonBuildPage(bool active)
    {
        m_BuildingsPage.SetActive(active);
        m_MainPage.SetActive(!active);
    }
}