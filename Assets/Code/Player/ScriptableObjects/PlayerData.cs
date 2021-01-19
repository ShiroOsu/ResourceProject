using UnityEngine;
//using System;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject//, ISerializationCallbackReceiver
{
    public LayerMask interactionLayer;


    // During game time the health of the player data will be "runTimeHealth"
    // To prevent any overrides to the initial health the player start with.
    //[NonSerialized] public float runTimeHealth;
    //public float health;
    //public void OnAfterDeserialize() { runTimeHealth = health; }
    //public void OnBeforeSerialize() { }
}