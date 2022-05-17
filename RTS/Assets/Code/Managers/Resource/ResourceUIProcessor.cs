using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Code.Enums;
using Code.ScriptableObjects;
using UnityEngine;

namespace Code.Managers.Resource
{
    public static class ResourceUIProcessor
    {
        private static Dictionary<ResourceType, ResourceUIManager> _resourceTypes = new();
        private static bool _initialized;

        private static void Initialize()
        {
            _resourceTypes.Clear();

            var allResourceTypes = Assembly.GetAssembly(typeof(ResourceUIManager)).GetTypes()
                .Where(t => typeof(ResourceUIManager).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var resource in allResourceTypes)
            {
                var resourceUIManager = Activator.CreateInstance(resource) as ResourceUIManager;
                _resourceTypes.Add(resourceUIManager.Type, resourceUIManager);
            }

            _initialized = true;
        }

        public static void EnableUIForResource(bool select, GameObject resource, ResourceType type,
            GameObject image, ResourceData data)
        {
            if (!_initialized) Initialize();

            var resourceUIManager = _resourceTypes[type];
            resourceUIManager.EnableMainUI(select, resource, type, image, data);
        }
    }
}