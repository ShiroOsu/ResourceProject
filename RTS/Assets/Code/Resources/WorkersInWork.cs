using System.Collections.Generic;
using Code.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Resources
{
    public class WorkersInWork : MonoBehaviour
    {
        public RawImage[] imageQueue;

        private readonly Queue<TextureAssetType> m_WorkQueue = new();
        
        // TODO: RemoveLast is Temp, should be remove clicked on, MAYBE
        protected void RemoveUnitInWork()
        {
           
        }

        public void ShowWindow(bool show)
        {
            gameObject.SetActive(show);
        }
    }
}