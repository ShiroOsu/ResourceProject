using System.Collections;
using Code.Managers;
using Code.Structures;
using Code.Tools.Debugging;
using Code.Tools.Enums;
using Code.UI;
using UnityEngine;

namespace Code.Timers
{
    public class BarracksTimer : CreateTimer
    {
        private TextureAssetType m_CurrentUnitToSpawn;
        
        public Barracks Barracks { get; set; }
        
        public override void AddActionOnSpawn(bool add)
        {
            if (add)
            {
                Barracks.OnSpawn += Spawn; // TODO: Delegate Allocation
            }
            else
            {
                Barracks.OnSpawn -= Spawn; // TODO: Delegate Allocation
            }
        }

        public override void TimerUpdate()
        {
            if (!Barracks)
                return;

            timerFill.maxValue = CurrentUnitTimeSpawn;
            
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
                Log.Print("BarracksTimer.cs", "Queue is full!");
                return;
            }
            
            SpawnQueue.Enqueue(type);
            imageQueue[i].texture = AllTextures.Instance.GetTexture(type);
            i++;

            if (!IsSpawning)
            {
                StartCoroutine(SpawnRoutine(Barracks.unitSpawnPoint.position, Barracks.FlagPoint));
            }
        }
        
        protected override IEnumerator SpawnRoutine(Vector3 startPos, Vector3 endPos)
        {
            IsSpawning = true;
            
            while (SpawnQueue.Count > 0)
            {
                CurrentTimeOnSpawn = 0f;
            
                var unitType = SpawnQueue.Dequeue();
                m_CurrentUnitToSpawn = unitType;
                
                var timeToSpawn = CurrentUnitTimeSpawn = GetUnitSpawnTime(m_CurrentUnitToSpawn);
            
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
                
                SpawnManager.Instance.SpawnUnit(unitType, Barracks.unitSpawnPoint.position, 
                     Barracks.FlagPoint);
                
                RemoveImageInQueue();
            }

            IsSpawning = false;
        }
    }
}
