using System.Collections;
using Code.Debugging;
using Code.Enums;
using Code.Managers;
using Code.Structures;
using Code.UI;
using UnityEngine;

namespace Code.Timers
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
            
            timerFill.maxValue = GetUnitSpawnTime(TextureAssetType.Builder);
            
            if (IsSpawning)
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

        protected override void Spawn(TextureAssetType type)
        {
            if (i >= imageQueue.Length)
            {
                Log.Print("CastleTimer.cs", "Queue is full!");
                return;
            }
            
            SpawnQueue.Enqueue(type);
            imageQueue[i].texture = AllTextures.Instance.GetTexture(type);
            i++;

            if (!IsSpawning)
            {
                StartCoroutine(SpawnRoutine(Castle.unitSpawnPoint.position, Castle.FlagPoint));
            }
        }
        
        protected override IEnumerator SpawnRoutine(Vector3 startPos, Vector3 endPos)
        {
            IsSpawning = true;

            while (SpawnQueue.Count > 0)
            {
                var timeToSpawn = GetUnitSpawnTime(TextureAssetType.Builder);
                CurrentTimeOnSpawn = 0f;

                var unitType = SpawnQueue.Dequeue();
            
                while (CurrentTimeOnSpawn < timeToSpawn)
                {
                    if (imageQueue[0].texture == null)
                    {
                        IsSpawning = false;
                        ClearTimer();
                        yield break;
                    }
                    
                    CurrentTimeOnSpawn += Time.deltaTime;
                    yield return null;
                }

                if (imageQueue[0].texture != null)
                {
                    SpawnManager.Instance.SpawnUnit(unitType, startPos, endPos);
                    RemoveImageInQueue();
                }
            }
            IsSpawning = false;
        }
    }
}