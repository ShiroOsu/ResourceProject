using Code.HelperClasses;
using Code.Managers.Units;
using Code.Player;
using Code.ScriptableObjects;
using Code.Units;
using UnityEngine;

namespace Code.Managers.Data
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
