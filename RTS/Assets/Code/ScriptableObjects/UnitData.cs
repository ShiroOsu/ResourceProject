using UnityEngine;

namespace Code.ScriptableObjects
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
        public float fieldOfView = 2f;
        [HideInInspector] public float acceleration = 16f;
        [HideInInspector] public float turnSpeed = 1f;

        [HideInInspector] public int navMeshSurfaceUnitID = 1479372276;
    }
}