using System;
using System.Collections;
using System.Collections.Generic;
using Code.Framework.Enums;
using Code.Framework.TextureListByEnum;
using Code.Managers;
using Code.Structures.Castle;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Framework
{
    public class CastleTimer : MonoBehaviour
    {
        [SerializeField] private GameObject m_Timer;
        [SerializeField] private RawImage m_CreateImage;
        [SerializeField] private Slider m_TimerFill;
        [SerializeField] private TextureList m_CreatableTextures;
        [SerializeField] private float m_SpawnTimeBuilder;

        private readonly Queue<UnitType> m_SpawnQueue = new Queue<UnitType>();
        private UnitType m_CurrentTypeToSpawn;
        private bool m_IsSpawning;
        private float m_CurrentTimeOnSpawn;

        public Castle m_Castle { get; set; }

        private void Awake()
        {
            ShowTimer(false);
        }

        public void AddActionOnSpawn()
        {
            m_Castle.OnSpawn += Spawn;
        }

        public void TimerUpdate()
        {
            if (!m_Castle || !(m_CurrentTimeOnSpawn > 0f))
                return;
            
            m_TimerFill.maxValue = m_SpawnTimeBuilder;
            
            if (m_IsSpawning)
            {
                m_CreateImage.texture = GetUnitTexture(m_CurrentTypeToSpawn);
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
            m_SpawnQueue.Enqueue(type);

            if (!m_IsSpawning)
            {
                StartCoroutine(SpawnRoutine());
            }
        }
        
        private IEnumerator SpawnRoutine()
        {
            m_IsSpawning = true;
            
            while (m_SpawnQueue.Count > 0)
            {
                float timeToSpawn = m_SpawnTimeBuilder;
                m_CurrentTimeOnSpawn = 0f;
            
                var unitType = m_SpawnQueue.Dequeue();
                m_CurrentTypeToSpawn = unitType;
            
                while (m_CurrentTimeOnSpawn < timeToSpawn)
                {
                    m_CurrentTimeOnSpawn += Time.deltaTime;
                    yield return null;
                }
                
                SpawnManager.Instance.SpawnUnit(unitType, m_Castle.m_UnitSpawnPoint.position, 
                    m_Castle.m_FlagPoint);
            }

            m_IsSpawning = false;
        }

        private void ShowTimer(bool show)
        {
            m_Timer.SetActive(show);

            if (!m_CreateImage.texture)
            {
                ShowImage(false);
            }
            else
            {
                ShowImage(true);
            }
        }

        private void ShowImage(bool show)
        {
            m_CreateImage.gameObject.SetActive(show);
        }

        // Temp
        private Texture GetUnitTexture(UnitType type)
        {
            var unitTexture = type switch
            {
                UnitType.Builder => m_CreatableTextures[TextureAssetType.Builder],
                UnitType.Solider => m_CreatableTextures[TextureAssetType.Solider],
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        
            return unitTexture;
        }
    }
}
