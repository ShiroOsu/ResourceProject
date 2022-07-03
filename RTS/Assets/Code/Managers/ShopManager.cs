using System;
using System.Collections.Generic;
using Code.Resources;
using Code.Tools.Debugging;
using Code.Tools.Enums;
using Code.Tools.HelperClasses;
using UnityEngine;

namespace Code.Managers
{
    public class ShopManager : Singleton<ShopManager>
    {
        private PlayerResources m_PlayerResources;
        
        [Serializable]
        public struct UnitCost
        {
            public UnitType unitType;
            public int gold;
            public int food;

            public bool Buy(PlayerResources pr)
            {
                if (pr.GetResourceInt(ResourceType.Gold) - gold < 0 ||
                    pr.GetResourceInt(ResourceType.Food) + food > 100) // Temp max value
                    return false;
                
                pr.AddResource(-gold, 0, 0, food);
                return true;
            }
        }
        
        [Serializable]
        public struct StructureCost
        {
            public StructureType structureType;
            public int gold;
            public int stone;
            public int wood;

            public bool Buy(PlayerResources pr)
            {
                if (pr.GetResourceInt(ResourceType.Gold) - gold < 0 ||
                    pr.GetResourceInt(ResourceType.Stone) - stone < 0 ||
                    pr.GetResourceInt(ResourceType.Wood) - wood < 0) 
                    return false;
                
                pr.AddResource(-gold, -stone, -wood);
                return true;
            }
        }

        private readonly Dictionary<StructureType, StructureCost> m_StructureCosts = new();
        [SerializeField] private StructureCost[] structureCosts;

        private readonly Dictionary<UnitType, UnitCost> m_UnitCosts = new();
        [SerializeField] private UnitCost[] unitCosts;

        public bool CanAffordUnit(UnitType type)
        {
            Log.Print("ShopManager.cs", "UnitType to buy: " + type);
            Log.Print("ShopManager.cs", $"Costs: gold:{m_UnitCosts[type].gold}, food:{m_UnitCosts[type].food}");
            
            return type switch
            {
                UnitType.Builder => m_UnitCosts[type].Buy(m_PlayerResources),
                UnitType.Soldier => m_UnitCosts[type].Buy(m_PlayerResources),
                UnitType.Horse => m_UnitCosts[type].Buy(m_PlayerResources),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
        
        public bool CanAffordBuilding(StructureType type)
        {
            Log.Print("ShopManager.cs", "StructureType to buy: " + type);
            Log.Print("ShopManager.cs", $"Costs: gold:{m_StructureCosts[type].gold}, " +
                                        $"Stone:{m_StructureCosts[type].stone}, Wood:{m_StructureCosts[type].wood}");
            
            return type switch
            {
                StructureType.Barracks => m_StructureCosts[type].Buy(m_PlayerResources),
                StructureType.Castle => m_StructureCosts[type].Buy(m_PlayerResources),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        private void CanAffordUpgrade()
        {
            
        }
        
        private void Awake()
        {
            m_PlayerResources = PlayerResources.Instance;
            SetupCosts();
        }

        private void SetupCosts()
        {
            foreach (var unit in unitCosts)
            {
                m_UnitCosts[unit.unitType] = unit;
            }

            foreach (var structure in structureCosts)
            {
                m_StructureCosts[structure.structureType] = structure;
            }
        }

        // TextureAssetType because that is the enum used for create timers 
        public UnitCost GetUnitCost(TextureAssetType type) => m_UnitCosts[(UnitType)type];
        public StructureCost GetStructureCost(TextureAssetType type) => m_StructureCosts[(StructureType)type];
    }
}
