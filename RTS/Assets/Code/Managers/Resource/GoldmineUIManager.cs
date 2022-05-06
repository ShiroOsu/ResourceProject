using Code.Enums;
using Code.HelperClasses;
using Code.Managers.UI;
using Code.Resources;
using Code.ScriptableObjects;
using Code.UI;
using UnityEngine;

namespace Code.Managers.Resource
{
    public class GoldmineUIManager : MonoBehaviour
    {
        private Goldmine m_GoldmineRef;
        //public StructureType Type => StructureType.Castle;

        public void EnableMainUI(bool active, GameObject resource, StructureType type, GameObject image, ResourceData data)
        {
            m_GoldmineRef = resource.ExGetComponent<Goldmine>();
            //UIManager.Instance.AddTimerToUI(m_GoldmineRef.castleTimer.timer, m_GoldmineRef.castleUIMiddle);
            UIManager.Instance.SetResourceStatsInfo(data);
            
            if (active)
            {
                //BindButtons();
            }

            //m_GoldmineRef.castleUIMiddle.SetActive(active);
            image.SetActive(active);

            // When de-select remove listeners
            if (!active)
            {
                MenuButtons.Instance.UnBindMenuButtons();
            }
        }

        // protected override void BindButtons()
        // {
        //     MenuButtons.Instance.BindMenuButton(m_GoldmineRef.OnSetSpawnFlagPosition, 0, 
        //         AllTextures.Instance.GetTexture(TextureAssetType.Flag));
        //     
        //     MenuButtons.Instance.BindMenuButton(m_GoldmineRef.OnSpawnBuilderButton, 4, 
        //         AllTextures.Instance.GetTexture(TextureAssetType.Builder));
        // }
    }
}
