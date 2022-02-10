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
                Castle.OnSpawn += Spawn;// TODO: Delegate Allocation
            }
            else
            {
                Castle.OnSpawn -= Spawn;// TODO: Delegate Allocation
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
                Log.Message("CastleTimer.cs", "Queue is full!");
                return;
            }
            
            SpawnQueue.Enqueue(type);
            imageQueue[i].texture = AllTextures.Instance.GetTexture(type);
            i++;

            if (!IsSpawning)
            {
                StartCoroutine(SpawnRoutine(Castle.unitSpawnPoint.position, Castle.FlagPoint));// TODO: Delegate Allocation
            }
        }
        
        protected override IEnumerator SpawnRoutine(Vector3 startPos, Vector3 endPos)
        {
            IsSpawning = true;

            while (SpawnQueue.Count > 0)
            {
                float timeToSpawn = GetUnitSpawnTime(TextureAssetType.Builder);
                CurrentTimeOnSpawn = 0f;

                var unitType = SpawnQueue.Dequeue();
            
                while (CurrentTimeOnSpawn < timeToSpawn)
                {
                    CurrentTimeOnSpawn += Time.deltaTime;
                    yield return null;
                }

                SpawnManager.Instance.SpawnUnit(unitType, startPos, endPos);  

                RemoveImageInQueue();
            }

            IsSpawning = false;
        }
    }
}
