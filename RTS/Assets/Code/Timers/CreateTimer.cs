using System;
using System.Collections;
using System.Collections.Generic;
using Code.Managers;
using Code.Resources;
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
        private PlayerResources m_PlayerResources;

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
            m_PlayerResources = PlayerResources.Instance;
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

        // TODO: Does not remove correct image when removed by button click ?
        protected virtual void RemoveFromQueueByButton()
        {
            if (SpawnQueue.Count > 0)
            {
                if (SpawnQueue.Count > 1)
                {
                    i--; // i = length of imageQueue, i--; is last element in array    
                }
                
                // Disable gameObject and remove texture
                imageQueue[i].gameObject.SetActive(false);
                imageQueue[i].texture = null;

                var queue = SpawnQueue.ToArray();

                // Give resources back when removing
                var lastInQueue = queue[^1];
                UnitResource(lastInQueue);
                
                // Re-set textures of remaining textures in imageQueue
                for (var j = 0; j < i; j++)
                {
                    imageQueue[j].texture = AllTextures.Instance.GetTexture(queue[j]);
                }
                
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

        // TODO: Does not remove correct image when removed by button click ?
        protected void RemoveImageInQueue()
        {
            i--; // i = length of imageQueue, i-- is last element in array
            // Disable gameObject and remove texture
            imageQueue[i].gameObject.SetActive(false);
            imageQueue[i].texture = null;

            var queue = SpawnQueue.ToArray();

            // Re-set textures of remaining textures in imageQueue
            for (var j = 0; j < i; j++)
            {
                imageQueue[j].texture = AllTextures.Instance.GetTexture(queue[j]);
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

        private void UnitResource(TextureAssetType type)
        {
            var costs = ShopManager.Instance.GetUnitCost(type);
            m_PlayerResources.AddResource(costs.gold, 0, 0, -costs.food);
        }

        private void OnDestroy() 
        {
            removeFromQueue.onClick.RemoveAllListeners();
        }
    }
}
