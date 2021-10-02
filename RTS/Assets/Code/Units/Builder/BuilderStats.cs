using UnityEngine;

namespace Code.Units.Builder
{
    [CreateAssetMenu(fileName = "BuilderStats", menuName = "ScriptableObjects/Units/BuilderStats")]
    public class BuilderStats : ScriptableObject
    {
        public string unitName = "Builder";
        public float health = 10f;
        public float mana = 5f;

        [Tooltip("Amount of damage blocked from attacks")]
        public int defense = 0;

        [Tooltip("Health regeneration per 5s")]
        public float hpRegen = 1f;

        public float movementSpeed = 2f;
        public float acceleration = 16f;
        public float turnSpeed = 1f;
    }
}
