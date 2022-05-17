using System.Collections;
using System.Collections.Generic;
using Code.Enums;
using Code.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Resources
{
    public class WorkersInWork : MonoBehaviour
    {
        public GameObject workersInWorkObject;
        public Button removeFromQueue;
        public RawImage[] imageQueue;
        protected int k;
        protected bool IsWorking;
        protected float CurrentTime;

        protected readonly Queue<TextureAssetType> SpawnQueue = new();

        private void Awake()
        {
            removeFromQueue.onClick.AddListener(RemoveFromQueueByButton);
        }

        public virtual void UpdateWorkers()
        {
            
        }

        private void RemoveFromQueueByButton()
        {
            if (SpawnQueue.Count > 0)
            {
                RemoveImageInQueue();
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

        protected virtual IEnumerator WorkRoutine()
        {
            yield return null;
        }
        

        protected void RemoveImageInQueue()
        {
            var i = SpawnQueue.Count; // i is equal to the length of m_SpawnQueue
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
            gameObject.SetActive(show);
        }

        private void OnDestroy()
        {
            removeFromQueue.onClick.RemoveAllListeners();
        }
    }
}