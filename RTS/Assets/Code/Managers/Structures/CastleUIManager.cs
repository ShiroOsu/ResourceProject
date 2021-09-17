using Code.Framework;
using Code.Framework.Enums;
using Code.Structures;
using Code.Structures.Castle;
using UnityEngine;

namespace Code.Managers.Structures
{
    public class CastleUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_CastleUIMiddle;
        [SerializeField] private GameObject m_Image;
        [SerializeField] private StructureData m_Data;

        private Castle m_CastleRef;
        private const string c_NameOfUIObjectInScene = "StructureInfo";

        public void EnableMainUI(bool active, GameObject structure)
        {
            m_CastleRef = structure.GetComponent<Castle>();
            UIManager.Instance.AddTimerToUI(m_CastleRef.CastleTimer.m_Timer, c_NameOfUIObjectInScene);
            UIManager.Instance.SetStructureStatsInfo(m_Data);
            
            if (active)
            {
                BindCastleButtons();
            }

            m_CastleUIMiddle.SetActive(active);
            m_Image.SetActive(active);

            // When de-select remove listeners
            if (!active)
            {
                MenuButtons.Instance.UnBindMenuButtons();
            }
        }

        private void BindCastleButtons()
        {
            MenuButtons.Instance.BindMenuButton(m_CastleRef.OnSetSpawnFlagPosition, 0, 
                AllTextures.Instance.GetButtonTexture(ButtonTexture.Flag));
            
            MenuButtons.Instance.BindMenuButton(m_CastleRef.OnSpawnBuilderButton, 4, 
                AllTextures.Instance.GetUnitTexture(UnitType.Builder));
        }
    }
}