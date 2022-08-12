using Code.Managers;
using Code.Tools.Enums;
using UnityEngine;

namespace Code.Tools
{
    public static class Tooltips
    {
        public static string Flag()
        {
            return "Units rally point";
        }
        
        public static string BuilderPage()
        {
            return "Open build page.";
        }

        public static string CloseBuildPage()
        {
            return "Close build page.";
        }

        public static string BuildStructure(StructureType type)
        {
            var cost = ShopManager.Instance.GetStructureCost(type);
            return $"Build {type}." +
                   $"\nGold: {cost.gold}" +
                   $"\nStone: {cost.stone}" +
                   $"\nWood: {cost.wood}";
        }

        public static string CreateUnit(TextureAssetType type)
        {
            var cost = ShopManager.Instance.GetUnitCost(type);
            return $"Create {type} unit." +
                   $"\nGold: {cost.gold}" +
                   $"\nFood: {cost.food}";
        }
    }
}