using Code.Managers.Units;
using Code.Player;
using Code.Player.Camera.ScriptableObjects;
using Code.Player.ScriptableObjects;
using Code.Structures.Castle;
using Code.Units;
using UnityEngine;

namespace Code.Managers
{
    public class DataManager : MonoBehaviour
    {
        private static DataManager s_Instance = null;
        public static DataManager Instance => s_Instance ??= FindObjectOfType<DataManager>();

        //public BuilderStats builderData;
        //public SoldierStats soldierData;
        [Header("Units")]
        public UnitData unitData;

        public UnitSpawnData SpawnData;

        [Header("Structures")]
        public CastleData castleData;

        [Header("Player")]
        public MouseData mouseData;
        public CameraData cameraData;
        public MouseInputs mouseInputs;
    }
}
