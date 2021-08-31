using System;
using System.Collections;
using System.Collections.Generic;
using Code.Framework.Enums;
using Code.Framework.TextureListByEnum;
using Code.Logger;
using Code.Managers;
using Code.Structures.Barracks;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Framework
{
    public class BarracksTimer : MonoBehaviour
    {
        [SerializeField] private GameObject m_Timer;
        [SerializeField] private Slider m_TimerFill;
        [SerializeField] private TextureList m_CreatableTextures;
        [SerializeField] private float m_SpawnTimeSoldier;
        [SerializeField] private RawImage[] m_ImageQueue;

        private readonly Queue<UnitType> m_SpawnQueue = new Queue<UnitType>();
        public bool IsSpawning { get; private set; }
        private float m_CurrentTimeOnSpawn;
        private int i = 0;

        public Barracks m_Barracks { get; set; }

        private void Awake()
        {
            ShowTimer(false);
        }

        public void AddActionOnSpawn(bool add)
        {
            if (add)
            {
                m_Barracks.OnSpawn += Spawn;
            }
            else
            {
                m_Barracks.OnSpawn -= Spawn;
            }
        }

        public void TimerUpdate()
        {
            if (!m_Barracks || !(m_CurrentTimeOnSpawn > 0f))
                return;
            
            // TODO: Change depending on what is in queue to spawn
            m_TimerFill.maxValue = m_SpawnTimeSoldier;
            
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
                Log.Message("BarracksTimer.cs", "Queue is full!");
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
                float timeToSpawn = m_SpawnTimeSoldier;
                m_CurrentTimeOnSpawn = 0f;
            
                var unitType = m_SpawnQueue.Dequeue();
            
                while (m_CurrentTimeOnSpawn < timeToSpawn)
                {
                    m_CurrentTimeOnSpawn += Time.deltaTime;
                    yield return null;
                }
                
                SpawnManager.Instance.SpawnUnit(unitType, m_Barracks.m_UnitSpawnPoint.position, 
                     m_Barracks.FlagPoint);
                
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
