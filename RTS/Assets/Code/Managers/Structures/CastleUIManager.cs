using System;
using System.Collections.Generic;
using Code.Framework;
using Code.Framework.Enums;
using Code.Framework.Extensions;
using Code.Structures.Castle;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Managers.Structures
{
    public class CastleUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_Image;
        [SerializeField] private GameObject m_Info;

        private Castle m_CastleRef;
        private const string c_NameOfUIObjectInScene = "CastleUIMiddle";

        public void EnableMainUI(bool active, GameObject structure)
        {
            m_CastleRef = structure.GetComponent<Castle>();
            UIManager.AddTimerToUI(m_CastleRef.CastleTimer.m_Timer, c_NameOfUIObjectInScene);
            
            if (active)
            {
                BindCastleButtons();
            }
            
            m_Image.SetActive(active);
            m_Info.SetActive(active);

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