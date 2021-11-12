using Code.Framework;
using Code.Managers.Units;
using Code.Player;
using Code.Player.Camera.ScriptableObjects;
using Code.Player.ScriptableObjects;
using Code.Units;
using UnityEngine;

namespace Code.Managers
{
    public class DataManager : Singleton<DataManager>
    {
        [Header("Units")]
        public UnitData unitData;

        public UnitSpawnData SpawnData;

        [Header("Player")]
        public MouseData mouseData;
        public CameraData cameraData;
        public MouseInputs mouseInputs;
    }
}
