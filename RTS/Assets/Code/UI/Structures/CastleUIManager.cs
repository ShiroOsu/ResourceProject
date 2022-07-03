using Code.ScriptableObjects;
using Code.Structures;
using Code.Tools.Enums;
using UnityEngine;

namespace Code.UI.Structures
{
    public class CastleUIManager : StructureUIManager
    {
        private Castle m_CastleRef;
        public override StructureType Type => StructureType.Castle;

        public override void EnableMainUI(bool active, GameObject structure, StructureType type, GameObject image, StructureData data)
        {
            m_CastleRef = structure.GetComponent<Castle>();
            UIManager.Instance.AddTimerToUI(m_CastleRef.castleTimer.timer, m_CastleRef.castleUIMiddle);
            UIManager.Instance.SetStructureStatsInfo(data, active);
            
            if (active)
            {
                BindButtons();
            }

            m_CastleRef.castleUIMiddle.SetActive(active);
            image.SetActive(active);

            // When de-select remove listeners
            if (!active)
            {
                MenuButtons.Instance.UnBindMenuButtons();
            }
        }

        protected override void BindButtons()
        {
            MenuButtons.Instance.BindMenuButton(m_CastleRef.OnSetSpawnFlagPosition, 0, 
                AllTextures.Instance.GetTexture(TextureAssetType.Flag), "", KeyCode.F);
            
            MenuButtons.Instance.BindMenuButton(m_CastleRef.OnSpawnBuilderButton, 4, 
                AllTextures.Instance.GetTexture(TextureAssetType.Builder), "", KeyCode.Q);
        }
    }
}