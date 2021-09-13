using System;
using System.Collections.Generic;
using Code.Framework;
using Code.Framework.Extensions;
using Code.Framework.Timers;
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
        [SerializeField] private GameObject m_BarracksTimer;
        
        [SerializeField] private List<Button> m_ButtonList = new List<Button>();

        private Barracks m_BarracksRef;

        public void EnableMainUI(bool active, GameObject structure)
        {
            m_BarracksRef = structure.GetComponent<Barracks>();
            
            if (!structure.TryGetComponent(out BarracksTimer _))
            {
                m_BarracksRef.BarracksTimer = structure.AddComponent<BarracksTimer>();
            }

            if (!structure.TryGetComponent(out EmptyScript _))
            {
                structure.AddComponent<EmptyScript>();
                
                m_BarracksRef.BarracksTimer.m_Timer = Instantiate(m_BarracksTimer, structure.transform);
                Extensions.SetUpCreateTimer(m_BarracksRef.BarracksTimer.m_Timer, "BarracksUIMiddle");
                m_BarracksRef.BarracksTimer.CreateTimer = m_BarracksRef.BarracksTimer.m_Timer.GetComponent<CreateTimer>();

                m_BarracksRef.BarracksTimer.m_TimerFill = m_BarracksRef.BarracksTimer.CreateTimer.m_TimerFill;
                m_BarracksRef.BarracksTimer.m_ImageQueue = m_BarracksRef.BarracksTimer.CreateTimer.m_ImageQueue;
                m_BarracksRef.BarracksTimer.m_SpawnTimeSoldier = m_BarracksRef.m_SpawnTimeSoldier;
                m_BarracksRef.BarracksTimer.m_SpawnTimeHorse = m_BarracksRef.m_SpawnTimeHorse;
            }

            m_ButtonList[0].onClick.AddListener(m_BarracksRef.SpawnSoldier);
            m_ButtonList[1].onClick.AddListener(m_BarracksRef.OnSetSpawnFlagPosition);
            m_ButtonList[2].onClick.AddListener(m_BarracksRef.SpawnHorse);

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
