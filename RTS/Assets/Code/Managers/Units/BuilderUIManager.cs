using Code.Framework.Enums;
using Code.Framework.UI;
using Code.Units;
using Code.Units.Builder;
using UnityEngine;

namespace Code.Managers.Units
{
    public class BuilderUIManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject m_Image;

        [SerializeField] private UnitData m_Data;

        private BuilderUnit m_Builder;
        public void EnableMainUI(bool active, GameObject unit)
        {
            m_Builder = unit.GetComponent<BuilderUnit>();
            UIManager.Instance.SetUnitStatsInfo(m_Data);

            if (active)
            {
                BindBuilderButtons();
            }
            
            m_Image.SetActive(active);

            if (!active)
            {
                MenuButtons.Instance.UnBindMenuButtons();
            }
        }

        private void OpenBuildPage()
        {
            MenuButtons.Instance.UnBindMenuButtons();
            BindBuildings();
        }

        private void BackToMainPage()
        {
            MenuButtons.Instance.UnBindMenuButtons();
            BindBuilderButtons();
        }

        private void BindBuilderButtons()
        {
            MenuButtons.Instance.BindMenuButton(OpenBuildPage, 15, 
                AllTextures.Instance.GetButtonTexture(ButtonTexture.Build));
        }

        private void BindBuildings()
        {
            MenuButtons.Instance.BindMenuButton(BackToMainPage, 15, AllTextures.Instance.GetUnitTexture(UnitType.Builder));
            
            MenuButtons.Instance.BindMenuButton(() => m_Builder.OnStructureBuildButton(StructureType.Barracks), 4, 
                AllTextures.Instance.GetStructureTexture(StructureType.Barracks));
        }
    }
}
