using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/SoldierStats")]
public class SoldierStats : ScriptableObject, ISerializationCallbackReceiver
{
    // Initial Values of a SoldierUnit
    public string unitName = "Soldier";
    public float health = 10f;

    [Tooltip("Amount of damage blocked from attacks")]
    public int defense = 2;
    
    [Tooltip("Health regeneration per 5s")]
    public float hpRegen = 1f;

    // During game time the health of the player data will be "runTimeHealth"
    // To prevent any overrides to the initial health the unit start with.
    [NonSerialized] public float runTimeHealth;
    [NonSerialized] public float runTimeHpRegen;
    [NonSerialized] public int runTimeDefense;

    public void OnAfterDeserialize() 
    {
        runTimeHealth = health;
        runTimeHpRegen = hpRegen;
        runTimeDefense = defense;
    }
    public void OnBeforeSerialize() { }
}
