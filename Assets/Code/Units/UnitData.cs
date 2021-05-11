using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObjects/Units/UnitData")]
public class UnitData : ScriptableObject, ISerializationCallbackReceiver
{
    public float maxHealth = 10f;

    [Tooltip("Amount of damage blocked from attacks")]
    public int defense = 2;

    [Tooltip("Health regeneration per 5s")]
    public float hpRegen = 1f;

    public float damage = 1f;
    public float attackSpeed = 1f;

    public float movementSpeed = 10f;
    public float acceleration = 16f;
    public float turnSpeed = 1f;

    public int soldierID = -1372625422;
    public int builderID = 0;

    // During game time the health & HpRegen of the unit data will be "runTimeHealth"
    // To prevent any overrides to the initial health the unit start with.
    [NonSerialized] public float runTimeHealth;

    public void OnAfterDeserialize()
    {
        runTimeHealth = maxHealth;
    }
    public void OnBeforeSerialize() { }
}
