using System.Collections;
using Code.Managers;
using Code.Timers;
using Code.Tools.Debugging;
using Code.Tools.Enums;
using Code.UI;
using UnityEngine;

namespace Code.Resources
{
    public class GoldmineWorkers : CreateTimer
    {
        public Goldmine Goldmine { get; set; }

        public bool AddWorker(TextureAssetType type)
        {
            timerFill.maxValue = C_TimeUntilHarvest;
            
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
                CurrentTimeOnSpawn = 0f;
            
                while (CurrentTimeOnSpawn < C_TimeUntilHarvest)
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

                Log.Print("GoldmineWorkers.cs", $"Added {SpawnQueue.Count * 10} amount of gold.");
                var goldAmount = SpawnQueue.Count * 10;
                Goldmine.ReduceResources((uint)goldAmount);
                PlayerResources.Instance.AddResource(goldAmount);
                
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
                timerFill.value = CurrentTimeOnSpawn;
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