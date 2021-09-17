using UnityEngine;

namespace Code.Structures
{
    [CreateAssetMenu(fileName = "StructureData", menuName = "ScriptableObjects/Structures/StructureData")]
    public class StructureData : ScriptableObject
    {
        public string structureName;
        public float health = 1000f;
        public float mana = 100f;
        public int armor = 10;
    }
}
