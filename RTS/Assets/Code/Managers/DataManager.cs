using Code.Camera;
using Code.Player;
using Code.ScriptableObjects;
using Code.Tools.HelperClasses;
using Code.Units;
using UnityEngine;

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
        public CameraControls cameraControls;
        public MouseInputs mouseInputs;
    }
}
