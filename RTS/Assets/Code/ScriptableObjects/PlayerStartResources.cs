using UnityEngine;

namespace Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerStartResources", menuName = "ScriptableObjects/Player/PlayerStartResources")]
    public class PlayerStartResources : ScriptableObject
    {
        public int gold;
        public int stone;
        public int wood;
        public int food;
    }
}
