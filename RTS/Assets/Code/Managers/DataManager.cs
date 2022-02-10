using Code.Framework;
using Code.Managers.Units;
using Code.Player;
using Code.Player.Camera.ScriptableObjects;
using Code.Player.ScriptableObjects;
using Code.Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Managers
{
    public class DataManager : Singleton<DataManager>
    {
        [Header("Units")]
        public UnitData unitData;

        public UnitSpawnData spawnData;

        [Header("Player")]
        public MouseData mouseData;
        public CameraData cameraData;
        public MouseInputs mouseInputs;
    }
}
