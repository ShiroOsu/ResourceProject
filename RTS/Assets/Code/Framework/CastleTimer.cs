using System;
using System.Collections;
using System.Collections.Generic;
using Code.Framework.Enums;
using Code.Framework.TextureListByEnum;
using Code.Logger;
using Code.Managers;
using Code.Structures.Castle;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Framework
{
    public class CastleTimer : MonoBehaviour
    {
        public GameObject m_Timer;
        [SerializeField] private Slider m_TimerFill;
        [SerializeField] private TextureList m_CreatableTextures;
        [SerializeField] private float m_SpawnTimeBuilder;
        [SerializeField] private RawImage[] m_ImageQueue;

        private readonly Queue<UnitType> m_SpawnQueue = new Queue<UnitType>();
        public bool IsSpawning { get; private set; }
        private float m_CurrentTimeOnSpawn;
        private int i = 0;

        public Castle m_Castle { get; set; }

        private void Awake()
        {
            ShowTimer(false);
        }

        public void AddActionOnSpawn(bool add)
        {
            if (add)
            {
                m_Castle.OnSpawn += Spawn;
            }
            else
            {
                m_Castle.OnSpawn -= Spawn;
            }
        }

        public void TimerUpdate()
        {
            if (!m_Castle || !(m_CurrentTimeOnSpawn > 0f))
                return;
            
            m_TimerFill.maxValue = m_SpawnTimeBuilder;
            
            if (IsSpawning)
            {
                foreach (var image in m_ImageQueue)
                {
                    image.gameObject.SetActive(image.texture != null);
                }
                
                ShowTimer(true);
                m_TimerFill.value = m_CurrentTimeOnSpawn;
            }
            else
            {
                ShowTimer(false);
            }
        }

        private void Spawn(UnitType type)
        {
            if (i >= m_ImageQueue.Length)
            {
                Log.Message("CastleTimer.cs", "Queue is full!");
                return;
            }
            
            m_SpawnQueue.Enqueue(type);
            m_ImageQueue[i].texture = AllTextures.Instance.GetUnitTexture(type);
            i++;

            if (!IsSpawning)
            {
                StartCoroutine(SpawnRoutine());
            }
        }
        
        private IEnumerator SpawnRoutine()
        {
            IsSpawning = true;
            
            while (m_SpawnQueue.Count > 0)
            {
                float timeToSpawn = m_SpawnTimeBuilder;
                m_CurrentTimeOnSpawn = 0f;

                var unitType = m_SpawnQueue.Dequeue();
            
                while (m_CurrentTimeOnSpawn < timeToSpawn)
                {
                    m_CurrentTimeOnSpawn += Time.deltaTime;
                    yield return null;
                }
                
                SpawnManager.Instance.SpawnUnit(unitType, m_Castle.m_UnitSpawnPoint.position, 
                    m_Castle.FlagPoint);

                RemoveImageInQueue();
            }

            IsSpawning = false;
        }

        private void RemoveImageInQueue()
        {
            i--;
            m_ImageQueue[i].gameObject.SetActive(false);
            m_ImageQueue[i].texture = null;
        }

        private void ShowTimer(bool show)
        {
            m_Timer.SetActive(show);
        }
    }
}
