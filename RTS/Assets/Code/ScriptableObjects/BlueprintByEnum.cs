using System.Collections.Generic;
using Code.Tools.Enums;
using UnityEngine;

namespace Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BlueprintByEnum", menuName = "ScriptableObjects/BlueprintByEnum")]
    public class BlueprintByEnum : ScriptableObject
    {
        [System.Serializable]
        public struct Blueprint
        {
            public GameObject blueprint;
            public StructureType structureType;
        }

        public Blueprint[] blueprints;
        private readonly Dictionary<StructureType, GameObject> m_Dictionary = new();

        public GameObject this[StructureType type]
        {
            get
            {
                Init();
                return m_Dictionary[type];
            }
        }

        private void Init()
        {
            foreach (var bp in blueprints)
            {
                m_Dictionary[bp.structureType] = bp.blueprint;
            }
        }
    }
}