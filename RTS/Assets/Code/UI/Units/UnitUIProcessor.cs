using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Code.ScriptableObjects;
using Code.Tools.Enums;
using Code.Units;
using UnityEngine;

namespace Code.UI.Units
{
    public static class UnitUIProcessor
    {
        private static Dictionary<UnitType, UnitUIManager> _unitTypes = new();
        private static bool _initialized;

        private static void Initialize()
        {
            _unitTypes.Clear();

            var allUnitTypes = Assembly.GetAssembly(typeof(UnitUIManager)).GetTypes()
                .Where(t => typeof(UnitUIManager).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach (var unit in allUnitTypes)
            {
                var unitUIManager = Activator.CreateInstance(unit) as UnitUIManager;
                    _unitTypes.Add(unitUIManager.Type, unitUIManager);
            }

            _initialized = true;
        }

        public static void EnableUIForUnit(bool select, GameObject unit, UnitType type, GameObject image, UnitData data)
        {
            if (!_initialized)
                Initialize();

            var unitUIManager = _unitTypes[type];
            unitUIManager.EnableMainUI(select, unit, type, image, data);
        }
    }
}