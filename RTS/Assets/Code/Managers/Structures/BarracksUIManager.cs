using Code.Enums;
using Code.Managers.UI;
using Code.ScriptableObjects;
using Code.Structures;
using Code.UI;
using UnityEngine;

namespace Code.Managers.Structures
{
    public class BarracksUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject image;
        [SerializeField] private GameObject info;
        [SerializeField] private StructureData data;

        private Barracks m_BarracksRef;
        private const string CNameOfUIObjectInScene = "BarracksUIMiddle";

        public void EnableMainUI(bool active, GameObject structure)
        {
            m_BarracksRef = structure.GetComponent<Barracks>();
            UIManager.Instance.AddTimerToUI(m_BarracksRef.barracksTimer.timer, CNameOfUIObjectInScene);
            UIManager.Instance.SetStructureStatsInfo(data);
            
            if (active)
            {
                BindBarracksButtons();
            }

            image.SetActive(active);
            info.SetActive(active);

            if (!active)
            {
                MenuButtons.Instance.UnBindMenuButtons();
            }
        }

        private void BindBarracksButtons()
        {
            MenuButtons.Instance.BindMenuButton(m_BarracksRef.OnSetSpawnFlagPosition, 0, 
                AllTextures.Instance.GetTexture(TextureAssetType.Flag));
            
            MenuButtons.Instance.BindMenuButton(m_BarracksRef.SpawnSoldier, 4, 
                AllTextures.Instance.GetTexture(TextureAssetType.Soldier));
            
            MenuButtons.Instance.BindMenuButton(m_BarracksRef.SpawnHorse, 3, 
                AllTextures.Instance.GetTexture(TextureAssetType.Horse));
        }
    }
}
