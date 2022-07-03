using Code.ScriptableObjects;
using Code.Structures;
using Code.Tools.Enums;
using UnityEngine;

namespace Code.UI.Structures
{
    public class BarracksUIManager : StructureUIManager
    {
        private Barracks m_BarracksRef;
        public override StructureType Type => StructureType.Barracks;

        
        public override void EnableMainUI(bool active, GameObject structure, StructureType type, GameObject image, StructureData data)
        {
            m_BarracksRef = structure.GetComponent<Barracks>();
            UIManager.Instance.AddTimerToUI(m_BarracksRef.barracksTimer.timer, m_BarracksRef.barracksUIMiddle);
            UIManager.Instance.SetStructureStatsInfo(data, active);
            
            if (active)
            {
                BindButtons();
            }

            image.SetActive(active);
            m_BarracksRef.barracksUIMiddle.SetActive(active);

            if (!active)
            {
                MenuButtons.Instance.UnBindMenuButtons();
            }
        }

        protected override void BindButtons()
        {
            MenuButtons.Instance.BindMenuButton(m_BarracksRef.OnSetSpawnFlagPosition, 0, 
                AllTextures.Instance.GetTexture(TextureAssetType.Flag), "", KeyCode.F);
            
            MenuButtons.Instance.BindMenuButton(m_BarracksRef.SpawnSoldier, 4, 
                AllTextures.Instance.GetTexture(TextureAssetType.Soldier), "", KeyCode.Q);
            
            MenuButtons.Instance.BindMenuButton(m_BarracksRef.SpawnHorse, 3, 
                AllTextures.Instance.GetTexture(TextureAssetType.Horse), "", KeyCode.E);
        }
    }
}