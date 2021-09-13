using System.Collections.Generic;
using Code.Framework;
using Code.Framework.Extensions;
using Code.Framework.Timers;
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
        [SerializeField] private GameObject m_CastleTimer;
        
        [SerializeField] private List<Button> m_ButtonList = new List<Button>();

        private Castle m_CastleRef;

        public void EnableMainUI(bool active, GameObject structure)
        {
            m_CastleRef = structure.GetComponent<Castle>();

            if (!structure.TryGetComponent(out CastleTimer _))
            {
                m_CastleRef.CastleTimer = structure.AddComponent<CastleTimer>();
            }

            if (!structure.TryGetComponent(out EmptyScript _))
            {
                structure.AddComponent<EmptyScript>();
                
                m_CastleRef.CastleTimer.m_Timer = Instantiate(m_CastleTimer, structure.transform);
                Extensions.SetUpCreateTimer(m_CastleRef.CastleTimer.m_Timer, "CastleUIMiddle");
                m_CastleRef.CastleTimer.CreateTimer = m_CastleRef.CastleTimer.m_Timer.GetComponent<CreateTimer>();

                m_CastleRef.CastleTimer.m_TimerFill = m_CastleRef.CastleTimer.CreateTimer.m_TimerFill;
                m_CastleRef.CastleTimer.m_ImageQueue = m_CastleRef.CastleTimer.CreateTimer.m_ImageQueue;
                m_CastleRef.CastleTimer.m_SpawnTimeBuilder = m_CastleRef.m_SpawnTimeBuilder;
            }
            
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
