using UnityEngine;

namespace Code.Units
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObjects/Units/UnitData")]
    public class UnitData : ScriptableObject
    {
        public string unitName;
        public float maxHealth = 10f;

        [Tooltip("Amount of damage blocked from attacks")]
        public int armor = 2;

        [Tooltip("Health regeneration per 5s")]
        [HideInInspector] public float hpRegen = 1f;

        public int attack = 1;
        public float attackSpeed = 1f;

        public float movementSpeed = 10f;
        [HideInInspector] public float acceleration = 16f;
        [HideInInspector] public float turnSpeed = 1f;

        [HideInInspector] public int soldierID = -1372625422;
        [HideInInspector] public int builderID = 0;
        [HideInInspector] public int horseID = -334000983;
    }
}