using Code.Enums;
using Code.HelperClasses;
using Code.Managers.UI;
using Code.Resources;
using Code.ScriptableObjects;
using Code.UI;
using UnityEngine;

namespace Code.Managers.Resource
{
    public class QuarryUIManager : ResourceUIManager
    {
        private Quarry m_QuarryRef;
        public override ResourceType Type => ResourceType.Stone;
        public override void EnableMainUI(bool active, GameObject resource, ResourceType type, GameObject image, ResourceData data)
        {
            m_QuarryRef = resource.ExGetComponent<Quarry>();
            UIManager.Instance.AddTimerToUI(m_QuarryRef.quarryWorkers.timer, m_QuarryRef.quarryUIMiddle);
            UIManager.Instance.SetResourceStatsInfo(data, active);
            
            if (active)
            {
                BindButtons();
            }

            m_QuarryRef.quarryUIMiddle.SetActive(active);
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
