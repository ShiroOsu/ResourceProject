using Code.Enums;
using UnityEngine;

namespace Code.SaveSystem.Data
{
    [System.Serializable]
    public struct CastleData
    {
        public Vector3 position;
        public Quaternion rotation;
            
        // Timer
        public float timerFillMaxValue; // The Time to spawn Current Type
        public float timerFillValue; // Current Time of timer
        public int imageQueueLength;
        public TextureAssetType[] textureAssetTypesInQueue;
            
        // Flag
        public Vector3 flagPosition;
    }
}