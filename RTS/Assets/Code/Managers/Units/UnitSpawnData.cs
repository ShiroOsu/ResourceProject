using UnityEngine;

namespace Code.Managers.Units
{
    [CreateAssetMenu(fileName = "UnitSpawnData", menuName = "ScriptableObjects/Units/UnitSpawnData")]
    public class UnitSpawnData : ScriptableObject
    {
        public float BuilderSpawnTime;
        public float SoldierSpawnTime;
        public float HorseSpawnTime;
    }
}
