using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Code.Framework.Enums;
using Code.Logger;
using Code.Managers;
using Code.Structures.Barracks;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace Code.Framework
{
    public class BarracksTimer : MonoBehaviour
    {
        [SerializeField] private GameObject m_Timer;
        [SerializeField] private Slider m_TimerFill;
        [SerializeField] private float m_SpawnTimeSoldier;
        [SerializeField] private float m_SpawnTimeHorse;
        [SerializeField] private RawImage[] m_ImageQueue;

        private readonly Queue<UnitType> m_SpawnQueue = new Queue<UnitType>();
        public bool IsSpawning { get; private set; }
        private float m_CurrentTimeOnSpawn;
        private UnitType m_CurrentUnitToSpawn;
        private float m_CurrentUnitTimeSpawn;
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

            m_TimerFill.maxValue = m_CurrentUnitTimeSpawn;
            
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

        private float GetUnitSpawnTime(UnitType type)
        {
            var unitTimer = type switch
            {
                UnitType.Solider => m_SpawnTimeSoldier,
                UnitType.Horse => m_SpawnTimeHorse,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            return unitTimer;
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
                m_CurrentTimeOnSpawn = 0f;
            
                var unitType = m_SpawnQueue.Dequeue();
                m_CurrentUnitToSpawn = unitType;
                
                float timeToSpawn = m_CurrentUnitTimeSpawn = GetUnitSpawnTime(m_CurrentUnitToSpawn);
            
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
            i--; // i is equal to the length of m_SpawnQueue
            // Remove last texture
            m_ImageQueue[i].gameObject.SetActive(false);
            m_ImageQueue[i].texture = null;
            
            // Grab m_SpawnQueue as Array
            var spawnQueue = m_SpawnQueue.ToArray();
            
            // Shifts all items in m_ImageQueue to the left 
            // and set texture to the next items in spawnQueue
            for (int j = 0; j < i; j++)
            {
                m_ImageQueue[j].texture = AllTextures.Instance.GetUnitTexture(spawnQueue[j]);
            }
        }

        private void ShowTimer(bool show)
        {
            m_Timer.SetActive(show);
        }
    }
}
