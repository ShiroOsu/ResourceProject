using System.Collections;
using Code.Framework.Enums;
using Code.Framework.Logger;
using Code.Framework.UI;
using Code.Managers;
using Code.Structures.Barracks;
using UnityEngine;

namespace Code.Framework.Timers
{
    public class BarracksTimer : CreateTimer
    {
        private TextureAssetType m_CurrentUnitToSpawn;
        
        public Barracks Barracks { get; set; }
        
        public override void AddActionOnSpawn(bool add)
        {
            if (add)
            {
                Barracks.OnSpawn += Spawn;
            }
            else
            {
                Barracks.OnSpawn -= Spawn;
            }
        }

        public override void TimerUpdate()
        {
            if (!Barracks)
                return;

            m_TimerFill.maxValue = m_CurrentUnitTimeSpawn;
            
            if (m_IsSpawning)
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

        protected override void Spawn(TextureAssetType type)
        {
            if (i >= m_ImageQueue.Length)
            {
                Log.Message("BarracksTimer.cs", "Queue is full!");
                return;
            }
            
            m_SpawnQueue.Enqueue(type);
            m_ImageQueue[i].texture = AllTextures.Instance.GetTexture(type);
            i++;

            if (!m_IsSpawning)
            {
                StartCoroutine(SpawnRoutine(Barracks.m_UnitSpawnPoint.position, Barracks.FlagPoint));
            }
        }
        
        protected override IEnumerator SpawnRoutine(Vector3 startPos, Vector3 endPos)
        {
            m_IsSpawning = true;
            
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
                
                SpawnManager.Instance.SpawnUnit(unitType, Barracks.m_UnitSpawnPoint.position, 
                     Barracks.FlagPoint);
                
                RemoveImageInQueue();
            }

            m_IsSpawning = false;
        }
    }
}
