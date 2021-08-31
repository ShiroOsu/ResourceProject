using System;
using System.Collections.Generic;
using Code.Framework;
using Code.Structures.Barracks;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Managers.Structures
{
    public class BarracksUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_Image;
        [SerializeField] private GameObject m_Info;
        [SerializeField] private GameObject m_UI;
        [SerializeField] private BarracksTimer m_BarracksTimer;
        
        [SerializeField] private List<Button> m_ButtonList = new List<Button>();

        private Barracks m_BarracksRef;

        private void Update()
        {
            m_BarracksTimer.TimerUpdate();
        }

        public void EnableMainUI(bool active, GameObject structure)
        {
            m_BarracksRef = structure.GetComponent<Barracks>();
            m_BarracksTimer.m_Barracks = m_BarracksRef;
            m_BarracksTimer.AddActionOnSpawn(active);

            m_ButtonList[0].onClick.AddListener(m_BarracksRef.SpawnSoldier);
            m_ButtonList[1].onClick.AddListener(m_BarracksRef.OnSetSpawnFlagPosition);

            m_Image.SetActive(active);
            m_Info.SetActive(active);
            m_UI.SetActive(active);

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
