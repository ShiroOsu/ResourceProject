using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BuilderStats", menuName = "ScriptableObjects/Units/BuilderStats")]
public class BuilderStats : ScriptableObject, ISerializationCallbackReceiver
{
    // Initial Values of a SoldierUnit
    public string unitName = "Builder";
    public float health = 10f;

    [Tooltip("Amount of damage blocked from attacks")]
    public int defense = 0;

    [Tooltip("Health regeneration per 5s")]
    public float hpRegen = 1f;

    //public float damage = 1f;
    //public float attackSpeed = 1f;
    //public float buildSpeed = 1f;

    public float movementSpeed = 2f;
    public float acceleration = 16f;
    public float turnSpeed = 1f;

    // During game time the health & HpRegen of the unit data will be "runTimeHealth"
    // To prevent any overrides to the initial health the unit start with.
    [NonSerialized] public float runTimeHealth;
    [NonSerialized] public float runTimeHpRegen;

    public void OnAfterDeserialize()
    {
        runTimeHealth = health;
        runTimeHpRegen = hpRegen;
    }
    public void OnBeforeSerialize() { }
}
