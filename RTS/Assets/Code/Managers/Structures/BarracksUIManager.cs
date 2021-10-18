using Code.Framework.Enums;
using Code.Framework.UI;
using Code.Structures;
using Code.Structures.Barracks;
using UnityEngine;

namespace Code.Managers.Structures
{
    public class BarracksUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_Image;
        [SerializeField] private GameObject m_Info;
        [SerializeField] private StructureData m_Data;

        private Barracks m_BarracksRef;
        private const string c_NameOfUIObjectInScene = "BarracksUIMiddle";

        public void EnableMainUI(bool active, GameObject structure)
        {
            m_BarracksRef = structure.GetComponent<Barracks>();
            UIManager.Instance.AddTimerToUI(m_BarracksRef.BarracksTimer.m_Timer, c_NameOfUIObjectInScene);
            UIManager.Instance.SetStructureStatsInfo(m_Data);
            
            if (active)
            {
                BindBarracksButtons();
            }

            m_Image.SetActive(active);
            m_Info.SetActive(active);

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
                AllTextures.Instance.GetTexture(TextureAssetType.Solider));
            
            MenuButtons.Instance.BindMenuButton(m_BarracksRef.SpawnHorse, 3, 
                AllTextures.Instance.GetTexture(TextureAssetType.Horse));
        }
    }
}
