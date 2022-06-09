using Code.Resources;
using Code.ScriptableObjects;
using Code.Tools.Enums;
using Code.Tools.HelperClasses;
using UnityEngine;

namespace Code.UI.Resource
{
    public class GoldmineUIManager : ResourceUIManager
    {
        private Goldmine m_GoldmineRef;
        public override ResourceType Type => ResourceType.Gold;

        public override void EnableMainUI(bool active, GameObject resource, ResourceType type, GameObject image, ResourceData data)
        {
            m_GoldmineRef = resource.ExGetComponent<Goldmine>();
            UIManager.Instance.AddTimerToUI(m_GoldmineRef.goldmineWorkers.timer, m_GoldmineRef.goldmineUIMiddle);
            UIManager.Instance.SetResourceStatsInfo(data, active);
            
            if (active)
            {
                BindButtons();
            }

            m_GoldmineRef.goldmineUIMiddle.SetActive(active);
            image.SetActive(active);

            // When de-select remove listeners
            if (!active)
            {
                MenuButtons.Instance.UnBindMenuButtons();
            }
        }

        protected override void BindButtons()
        {
            // MenuButtons.Instance.BindMenuButton(m_GoldmineRef.OnDestroyMine, 1,
            //     AllTextures.Instance.GetTexture(TextureAssetType.Destroy));
        }
    }
}
