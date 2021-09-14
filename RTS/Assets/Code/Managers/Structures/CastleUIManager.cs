using System.Collections.Generic;
using Code.Framework;
using Code.Structures.Castle;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Managers.Structures
{
    public class CastleUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_Image;
        [SerializeField] private GameObject m_Info;
        [SerializeField] private GameObject m_UI;

        [SerializeField] private List<Button> m_ButtonList = new List<Button>();

        private Castle m_CastleRef;
        private const string c_NameOfUIObjectInScene = "CastleUIMiddle";

        public void EnableMainUI(bool active, GameObject structure)
        {
            m_CastleRef = structure.GetComponent<Castle>();

            UIManager.AddTimerToUI(m_CastleRef.CastleTimer.m_Timer, c_NameOfUIObjectInScene);

            m_ButtonList[0].onClick.AddListener(m_CastleRef.OnSpawnBuilderButton);
            m_ButtonList[1].onClick.AddListener(m_CastleRef.OnSetSpawnFlagPosition);

            m_Image.SetActive(active);
            m_Info.SetActive(active);
            m_UI.SetActive(active);

            // When de-select remove listeners so don't add them more than once
            if (!active)
            {
                RemoveAllListeners();
            }
        }

        private void RemoveAllListeners()
        {
            foreach (var button in m_ButtonList)
            {
                button.onClick.RemoveAllListeners();
            }
        }
    }
}