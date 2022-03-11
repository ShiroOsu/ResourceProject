using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Code.Enums;
using Code.ScriptableObjects;
using UnityEngine;

namespace Code.Managers.Structures
{
    public static class StructureUIProcessor
    {
        private static Dictionary<StructureType, StructureUIManager> _structureTypes = new();
        private static bool _initialized;

        private static void Initialize()
        {
            _structureTypes.Clear();

            var allStructureTypes = Assembly.GetAssembly(typeof(StructureUIManager)).GetTypes()
                .Where(t => typeof(StructureUIManager).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var structure in allStructureTypes)
            {
                var structureUIManager = Activator.CreateInstance(structure) as StructureUIManager;
                _structureTypes.Add(structureUIManager.Type, structureUIManager);
            }

            _initialized = true;
        }

        public static void EnableUIForStructure(bool select, GameObject structure, StructureType type,
            GameObject image, StructureData data)
        {
            if (!_initialized)
                Initialize();

            var structureUIManager = _structureTypes[type];
            structureUIManager.EnableMainUI(select, structure, type, image, data);
        }
    }
}
