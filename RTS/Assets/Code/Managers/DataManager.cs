using Code.Managers.Units;
using Code.Player;
using Code.Player.Camera.ScriptableObjects;
using Code.Player.ScriptableObjects;
using Code.Units;
using UnityEngine;

namespace Code.Managers
{
    public class DataManager : MonoBehaviour
    {
        private static DataManager s_Instance = null;
        public static DataManager Instance => s_Instance ??= FindObjectOfType<DataManager>();

        [Header("Units")]
        public UnitData unitData;

        public UnitSpawnData SpawnData;

        [Header("Player")]
        public MouseData mouseData;
        public CameraData cameraData;
        public MouseInputs mouseInputs;
    }
}
