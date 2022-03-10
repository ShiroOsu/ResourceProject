using Code.Enums;
using Code.Managers.UI;
using Code.ScriptableObjects;
using Code.Structures;
using Code.UI;
using UnityEngine;

namespace Code.Managers.Structures
{
    public class CastleUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject castleUIMiddle;
        [SerializeField] private GameObject image;
        [SerializeField] private StructureData data;

        private Castle m_CastleRef;
        private const string CNameOfUIObjectInScene = "StructureInfo";

        public void EnableMainUI(bool active, GameObject structure)
        {
            m_CastleRef = structure.GetComponent<Castle>();
            UIManager.Instance.AddTimerToUI(m_CastleRef.castleTimer.timer, CNameOfUIObjectInScene);
            UIManager.Instance.SetStructureStatsInfo(data);
            
            if (active)
            {
                BindCastleButtons();
            }

            castleUIMiddle.SetActive(active);
            image.SetActive(active);

            // When de-select remove listeners
            if (!active)
            {
                MenuButtons.Instance.UnBindMenuButtons();
            }
        }

        private void BindCastleButtons()
        {
            MenuButtons.Instance.BindMenuButton(m_CastleRef.OnSetSpawnFlagPosition, 0, 
                AllTextures.Instance.GetTexture(TextureAssetType.Flag));
            
            MenuButtons.Instance.BindMenuButton(m_CastleRef.OnSpawnBuilderButton, 4, 
                AllTextures.Instance.GetTexture(TextureAssetType.Builder));
        }
    }
}