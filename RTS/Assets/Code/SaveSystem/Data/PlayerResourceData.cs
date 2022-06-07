using Code.Interfaces;
using Code.Resources;

namespace Code.SaveSystem.Data
{
    [System.Serializable]
    public class PlayerResourceData : IPlayerResourceData
    {
        public int gold;
        public int stone;
        public int wood;
        public int food;
        public int units;

        public void Save(int gold, int stone, int wood, int food, int units)
        {
            this.gold = gold;
            this.stone = stone;
            this.wood = wood;
            this.food = food;
            this.units = units;
        }

        public void LoadResources()
        {
            PlayerResources.Instance.AddResource(gold, stone, wood, food, units);
        }
    }
}
