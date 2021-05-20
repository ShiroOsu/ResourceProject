using Code.Framework.Enums;
using Code.Units.Builder;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Managers.Units
{
    public class BuilderUIManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject m_BuildingsPage;
        [SerializeField] private GameObject m_MainPage;
        [SerializeField] private GameObject m_Image;
        [SerializeField] private GameObject m_UI;
        [SerializeField] private Button m_BuildButton;

        [Header("Buildings")]
        [SerializeField] private Button m_BarracksButton;

        private BuilderUnit m_BuilderUnit;

        public void EnableMainUI(bool active, GameObject unit)
        {
            m_BuilderUnit = unit.GetComponent<BuilderUnit>();
            m_BuildButton.onClick.AddListener(() => OnButtonBuildPage(active));

            m_BarracksButton.onClick.AddListener(() => BuilderUnit.OnStructureBuildButton(StructureType.Barracks));

            m_MainPage.SetActive(active);
            m_Image.SetActive(active);
            m_UI.SetActive(active);

            if (!(active is false)) return;

            OnButtonBuildPage(false);

            m_BuildButton.onClick.RemoveAllListeners();
            m_BarracksButton.onClick.RemoveAllListeners();
        }

        public void OnButtonBuildPage(bool active)
        {
            m_BuildingsPage.SetActive(active);
            m_MainPage.SetActive(!active);
        }
    }
}
