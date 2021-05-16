using UnityEngine;

namespace Code.Structures.Castle
{
    [CreateAssetMenu(fileName = "CastleData", menuName = "ScriptableObjects/Structures/CastleData")]
    public class CastleData : ScriptableObject
    {
        public float maxHealth = 100f;
        public float defense = 10f;
        public float attack = 5f;
    }
}
