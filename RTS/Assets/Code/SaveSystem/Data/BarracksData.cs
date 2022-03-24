using Code.Enums;
using UnityEngine;

namespace Code.SaveSystem.Data
{
    [System.Serializable]
    public struct BarracksData
    {
        public Vector3 position;
        public Quaternion rotation;
            
        // Timer
        public float timerValue;
        public TextureAssetType[] imageQueue;
            
        // Flag
        public Vector3 flagPosition;
    }
}