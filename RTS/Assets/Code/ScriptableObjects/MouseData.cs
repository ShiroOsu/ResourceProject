using UnityEngine;

//using System;

namespace Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = "MouseData", menuName = "ScriptableObjects/Player/MouseData")]
    public class MouseData : ScriptableObject//, ISerializationCallbackReceiver
    {
        public LayerMask unitMask;
        public LayerMask structureMask;
        public LayerMask groundMask;

        // During game time the health of the player data will be "runTimeHealth"
        // To prevent any overrides to the initial health the player start with.
        //[NonSerialized] public float runTimeHealth;
        //public float health;
        //public void OnAfterDeserialize() { runTimeHealth = health; }
        //public void OnBeforeSerialize() { }
    }
}
