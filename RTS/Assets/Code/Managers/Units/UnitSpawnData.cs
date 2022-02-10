using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Managers.Units
{
    [CreateAssetMenu(fileName = "UnitSpawnData", menuName = "ScriptableObjects/Units/UnitSpawnData")]
    public class UnitSpawnData : ScriptableObject
    {
        public float builderSpawnTime;
        public float soldierSpawnTime;
        public float horseSpawnTime;
    }
}
