using Code.Enums;
using Code.Managers.UI;
using Code.UI;
using Code.Units;
using Code.Units.Builder;
using UnityEngine;

namespace Code.Managers.Units
{
    public class BuilderUIManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject image;
        [SerializeField] private UnitData data;

        private BuilderUnit m_Builder;
        public void EnableMainUI(bool active, GameObject unit)
        {
            m_Builder = unit.GetComponent<BuilderUnit>();
            UIManager.Instance.SetUnitStatsInfo(data);

            if (active)
            {
                BindBuilderButtons();
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
            BindBuilderButtons();
        }

        private void BindBuilderButtons()
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
