using System.Collections.Generic;
using Code.Framework.Enums;
using UnityEngine;

namespace Code.Framework
{
    [CreateAssetMenu(fileName = "BlueprintByEnum", menuName = "ScriptableObjects/BlueprintByEnum")]
    public class BlueprintByEnum : ScriptableObject
    {
        [System.Serializable]
        public struct Blueprint
        {
            public GameObject m_Blueprint;
            public StructureType m_StructureType;
        }

        public Blueprint[] m_Blueprints;
        private readonly Dictionary<StructureType, GameObject> m_Dictionary = 
            new Dictionary<StructureType, GameObject>();

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
            foreach (var bp in m_Blueprints)
            {
                m_Dictionary[bp.m_StructureType] = bp.m_Blueprint;
            }
        }
    }
}