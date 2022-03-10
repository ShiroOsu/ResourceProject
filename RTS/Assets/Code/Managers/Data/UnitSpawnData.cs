using UnityEngine;

namespace Code.Managers.Data
{
    [CreateAssetMenu(fileName = "UnitSpawnData", menuName = "ScriptableObjects/Units/UnitSpawnData")]
    public class UnitSpawnData : ScriptableObject
    {
        public float builderSpawnTime;
        public float soldierSpawnTime;
        public float horseSpawnTime;
    }
}
