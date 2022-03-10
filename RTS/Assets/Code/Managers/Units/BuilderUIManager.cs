using Code.Enums;
using Code.Managers.UI;
using Code.UI;
using Code.Units;
using Code.Units.Builder;
using UnityEngine;

namespace Code.Managers.Units
{
    public class BuilderUIManager : UnitUIManager
    {
        private BuilderUnit m_Builder;
        public override UnitType Type => UnitType.Builder;

        
        public override void EnableMainUI(bool active, GameObject unit, UnitType type, GameObject image, UnitData data)
        {
            m_Builder = unit.GetComponent<BuilderUnit>();
            UIManager.Instance.SetUnitStatsInfo(data);

            if (active)
            {
                BindButtons();
            }
            
            image.SetActive(active);

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
            BindButtons();
        }

        protected override void BindButtons()
        {
            MenuButtons.Instance.BindMenuButton(OpenBuildPage, 15, 
                AllTextures.Instance.GetTexture(TextureAssetType.Build));
        }

        private void BindBuildings()
        {
            MenuButtons.Instance.BindMenuButton(BackToMainPage, 15, AllTextures.Instance.GetTexture(TextureAssetType.Builder));
            
            MenuButtons.Instance.BindMenuButton(() => m_Builder.OnStructureBuildButton(StructureType.Barracks), 4, 
                AllTextures.Instance.GetTexture(TextureAssetType.Barracks));
        }
    }
}
