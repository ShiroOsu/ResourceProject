using Code.Enums;
using Code.Managers.UI;
using Code.ScriptableObjects;
using Code.Structures;
using Code.UI;
using UnityEngine;

namespace Code.Managers.Structures
{
    public class CastleUIManager : StructureUIManager
    {
        private Castle m_CastleRef;
        public override StructureType Type => StructureType.Castle;

        public override void EnableMainUI(bool active, GameObject structure, StructureType type, GameObject image, StructureData data)
        {
            m_CastleRef = structure.GetComponent<Castle>();
            // TODO: AddTimerToUI() in the end calls Extensions.FindObject() with the name, but we can send the object instead of the name
            UIManager.Instance.AddTimerToUI(m_CastleRef.castleTimer.timer, m_CastleRef.NameOfUIObjectInScene);
            UIManager.Instance.SetStructureStatsInfo(data);
            
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
                AllTextures.Instance.GetTexture(TextureAssetType.Flag));
            
            MenuButtons.Instance.BindMenuButton(m_CastleRef.OnSpawnBuilderButton, 4, 
                AllTextures.Instance.GetTexture(TextureAssetType.Builder));
        }
    }
}