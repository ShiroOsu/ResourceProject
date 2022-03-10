using System;
using System.Collections;
using System.Collections.Generic;
using Code.Enums;
using Code.Managers;
using Code.Managers.Data;
using Code.UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.Timers
{
    public class CreateTimer : MonoBehaviour
    {
        [FormerlySerializedAs("m_Timer")] public GameObject timer;
        [FormerlySerializedAs("m_TimerFill")] public Slider timerFill;
        [FormerlySerializedAs("m_ImageQueue")] public RawImage[] imageQueue;

        protected readonly Queue<TextureAssetType> SpawnQueue = new Queue<TextureAssetType>();
        protected float CurrentUnitTimeSpawn;
        protected bool IsSpawning;
        protected float CurrentTimeOnSpawn;
        protected int i = 0;

        public virtual void AddActionOnSpawn(bool add)
        {
            
        }

        public virtual void TimerUpdate()
        {
            
        }

        protected virtual void Spawn(TextureAssetType type)
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
            imageQueue[i].gameObject.SetActive(false);
            imageQueue[i].texture = null;
            
            // Grab m_SpawnQueue as Array
            var spawnQueue = SpawnQueue.ToArray();
            
            // Shifts all items in m_ImageQueue to the left 
            // and set texture to the next items in spawnQueue
            for (var j = 0; j < i; j++)
            {
                imageQueue[j].texture = AllTextures.Instance.GetTexture(spawnQueue[j]);
            }
        }

        protected void ShowTimer(bool show)
        {
            timer.SetActive(show);
        }

        protected static float GetUnitSpawnTime(TextureAssetType type)
        {
            var unitTime = type switch
            {
                TextureAssetType.Builder => DataManager.Instance.spawnData.builderSpawnTime,
                TextureAssetType.Solider => DataManager.Instance.spawnData.soldierSpawnTime,
                TextureAssetType.Horse => DataManager.Instance.spawnData.horseSpawnTime,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            return unitTime;
        }
    }
}
