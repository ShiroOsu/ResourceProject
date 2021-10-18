using System.Collections;
using Code.Framework.Enums;
using Code.Framework.Logger;
using Code.Framework.UI;
using Code.Managers;
using Code.Structures.Castle;
using UnityEngine;

namespace Code.Framework.Timers
{
    public class CastleTimer : CreateTimer
    {
        public Castle Castle { get; set; }

        public override void AddActionOnSpawn(bool add)
        {
            if (add)
            {
                Castle.OnSpawn += Spawn;
            }
            else
            {
                Castle.OnSpawn -= Spawn;
            }
        }

        public override void TimerUpdate()
        {
            if (!Castle)
                return;
            
            m_TimerFill.maxValue = GetUnitSpawnTime(TextureAssetType.Builder);
            
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
                Log.Message("CastleTimer.cs", "Queue is full!");
                return;
            }
            
            m_SpawnQueue.Enqueue(type);
            m_ImageQueue[i].texture = AllTextures.Instance.GetTexture(type);
            i++;

            if (!m_IsSpawning)
            {
                StartCoroutine(SpawnRoutine(Castle.m_UnitSpawnPoint.position, Castle.FlagPoint));
            }
        }
        
        protected override IEnumerator SpawnRoutine(Vector3 startPos, Vector3 endPos)
        {
            m_IsSpawning = true;

            while (m_SpawnQueue.Count > 0)
            {
                float timeToSpawn = GetUnitSpawnTime(TextureAssetType.Builder);
                m_CurrentTimeOnSpawn = 0f;

                var unitType = m_SpawnQueue.Dequeue();
            
                while (m_CurrentTimeOnSpawn < timeToSpawn)
                {
                    m_CurrentTimeOnSpawn += Time.deltaTime;
                    yield return null;
                }

                SpawnManager.Instance.SpawnUnit(unitType, startPos, endPos);  

                RemoveImageInQueue();
            }

            m_IsSpawning = false;
        }
    }
}
