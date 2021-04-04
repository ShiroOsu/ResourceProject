using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SoldierStats", menuName = "ScriptableObjects/Units/SoldierStats")]
public class SoldierStats : ScriptableObject, ISerializationCallbackReceiver
{
    // Initial Values of a SoldierUnit
    public string unitName = "Soldier";
    public float health = 10f;

    [Tooltip("Amount of damage blocked from attacks")]
    public int defense = 2;
    
    [Tooltip("Health regeneration per 5s")]
    public float hpRegen = 1f;

    public float damage = 1f;
    public float attackSpeed = 1f;

    public float movementSpeed = 2f;
    public float acceleration = 16f;

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
