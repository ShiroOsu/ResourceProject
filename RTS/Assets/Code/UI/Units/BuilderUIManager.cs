using Code.Tools.Enums;
using Code.Units;
using Code.Units.Builder;
using UnityEngine;

namespace Code.UI.Units
{
    public class BuilderUIManager : UnitUIManager
    {
        private BuilderUnit m_Builder;
        public override UnitType Type => UnitType.Builder;

        
        public override void EnableMainUI(bool active, GameObject unit, UnitType type, GameObject image, UnitData data)
        {
            m_Builder = unit.GetComponent<BuilderUnit>();
            UIManager.Instance.SetUnitStatsInfo(data, active);

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
                AllTextures.Instance.GetTexture(TextureAssetType.Build), KeyCode.A);
        }

        private void BindBuildings()
        {
            MenuButtons.Instance.BindMenuButton(BackToMainPage, 15, 
                AllTextures.Instance.GetTexture(TextureAssetType.Builder), KeyCode.B);
            
            MenuButtons.Instance.BindMenuButton(() => m_Builder.OnStructureBuildButton(StructureType.Barracks), 4, 
                AllTextures.Instance.GetTexture(TextureAssetType.Barracks), KeyCode.J);
        }
    }
}
