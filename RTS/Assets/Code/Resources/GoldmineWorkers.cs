using System.Collections;
using Code.Debugging;
using Code.Enums;
using Code.Managers;
using Code.Timers;
using Code.UI;
using UnityEngine;

namespace Code.Resources
{
    public class GoldmineWorkers : CreateTimer
    {
        public Goldmine Goldmine { get; set; }

        public bool AddWorker(TextureAssetType type)
        {
            if (i >= imageQueue.Length)
            {
                Log.Print("GoldmineWorkers.cs", "Goldmine is full!");
                return false;
            }

            SpawnQueue.Enqueue(type);
            imageQueue[i].texture = AllTextures.Instance.GetTexture(type);
            i++;

            if (!IsWorking)
            {
                StartCoroutine(WorkRoutine());
            }

            return true;
        }

        protected override IEnumerator WorkRoutine()
        {
            IsWorking = true;

            while (SpawnQueue.Count > 0)
            {
                const float timeToSpawn = 2f;
                CurrentTimeOnSpawn = 0f;
            
                while (CurrentTimeOnSpawn < timeToSpawn)
                {
                    if (imageQueue[0].texture == null)
                    {
                        IsWorking = false;
                        ClearTimer();
                        yield break;
                    }
                    
                    CurrentTimeOnSpawn += Time.deltaTime;
                    yield return null;
                }

                Log.Print("GoldmineWorkers.cs", "Added X amount of gold.");
                Goldmine.ReduceResources((uint)SpawnQueue.Count * 10);
                // PlayerResources.Instance.AddGold(10 * SpawnQueue.Count * (UpgradeLevel));
                
            }
            IsWorking = false;
        }

        public override void TimerUpdate()
        {
            if (IsWorking)
            {
                foreach (var image in imageQueue)
                {
                    image.gameObject.SetActive(image.texture != null);
                }
                
                ShowTimer(true);
            }
            else
            {
                ShowTimer(false);
            }
        }

        protected override void RemoveFromQueueByButton()
        {
            var pos = Goldmine.removedUnitsSpawnPos.position;
            SpawnManager.Instance.SpawnUnit(SpawnQueue.Dequeue(), pos, pos);
            RemoveImageInQueue();
        }
    }
}