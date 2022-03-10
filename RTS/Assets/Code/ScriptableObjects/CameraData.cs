using UnityEngine;

namespace Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CameraData", menuName = "ScriptableObjects/Player/CameraData")]
    public class CameraData : ScriptableObject
    {
        // This might be moved to the ScriptableObject 'PlayerData'

        public float cameraSpeed;
        public float rotationSpeed;
        public float zoomSpeed;
    }
}
