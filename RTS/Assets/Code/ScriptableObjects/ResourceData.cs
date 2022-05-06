using UnityEngine;

namespace Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ResourceData", menuName = "ScriptableObjects/Resources/ResourceData")]
    public class ResourceData : ScriptableObject
    {
        public string resourceName;
        public uint resourcesLeft;
        public int armor = 10;
    }
}
