using System;
using System.Collections;
using System.Collections.Generic;
using Code.Managers;
using Code.Tools.Enums;
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
        public Button removeFromQueue;
        [FormerlySerializedAs("m_ImageQueue")] public RawImage[] imageQueue;

        protected readonly Queue<TextureAssetType> SpawnQueue = new();
        protected float CurrentUnitTimeSpawn;
        protected const float C_TimeUntilHarvest = 2f;
        protected bool IsSpawning;
        protected bool IsWorking;
        protected float CurrentTimeOnSpawn;
        protected int i;

        private void Awake()
        {
            removeFromQueue.onClick.AddListener(RemoveFromQueueByButton);
        }

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

        protected virtual IEnumerator WorkRoutine()
        {
            return null;
        }

        protected virtual void RemoveFromQueueByButton()
        {
            if (SpawnQueue.Count > 0)
            {
                RemoveImageInQueue();
                CurrentTimeOnSpawn = 0f;
            }
        }

        protected void ClearTimer()
        {
            if (imageQueue[0].texture == null)
            {
                SpawnQueue.Clear();
                ShowTimer(false);
            }
        }

        // TODO: Does not remove correct image when removed by button click
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
                TextureAssetType.Soldier => DataManager.Instance.spawnData.soldierSpawnTime,
                TextureAssetType.Horse => DataManager.Instance.spawnData.horseSpawnTime,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            return unitTime;
        }

        private void OnDestroy()
        {
            removeFromQueue.onClick.RemoveAllListeners();
        }
    }
}
