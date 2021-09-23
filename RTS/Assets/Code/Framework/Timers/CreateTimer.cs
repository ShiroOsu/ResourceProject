using System;
using System.Collections;
using System.Collections.Generic;
using Code.Framework.Enums;
using Code.Framework.UI;
using Code.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Framework.Timers
{
    public class CreateTimer : MonoBehaviour
    {
        public GameObject m_Timer;
        public Slider m_TimerFill;
        public RawImage[] m_ImageQueue;

        protected readonly Queue<UnitType> m_SpawnQueue = new Queue<UnitType>();
        protected float m_CurrentUnitTimeSpawn;
        protected bool m_IsSpawning;
        protected float m_CurrentTimeOnSpawn;
        protected int i = 0;

        public virtual void AddActionOnSpawn(bool add)
        {
            
        }

        public virtual void TimerUpdate()
        {
            
        }

        protected virtual void Spawn(UnitType type)
        {
            
        }

        protected virtual IEnumerator SpawnRoutine(Vector3 startPos, Vector3 endPos)
        {
            return null;
        }

        protected void RemoveImageInQueue()
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

        protected void ShowTimer(bool show)
        {
            m_Timer.SetActive(show);
        }

        protected static float GetUnitSpawnTime(UnitType type)
        {
            var unitTime = type switch
            {
                UnitType.Builder => DataManager.Instance.SpawnData.BuilderSpawnTime,
                UnitType.Solider => DataManager.Instance.SpawnData.SoldierSpawnTime,
                UnitType.Horse => DataManager.Instance.SpawnData.HorseSpawnTime,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            return unitTime;
        }
    }
}
