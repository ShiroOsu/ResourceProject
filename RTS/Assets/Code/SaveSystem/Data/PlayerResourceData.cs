using System;
using Code.Enums;
using Code.Interfaces;
using Code.Resources;

namespace Code.SaveSystem.Data
{
    [Serializable]
    public class PlayerResourceData : IPlayerResourceData
    {
        public int gold;
        public int stone;
        public int wood;
        public int food;

        public void Save(int gold, int stone, int wood, int food)
        {
            this.gold = gold;
            this.stone = stone;
            this.wood = wood;
            this.food = food;
        }

        public void LoadResources()
        {
            PlayerResources.Instance.AddResource(gold, stone, wood, food);
        }
    }
}
