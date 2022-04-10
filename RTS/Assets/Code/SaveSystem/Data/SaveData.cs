using System.Collections.Generic;

namespace Code.SaveSystem.Data
{
    [System.Serializable]
    public class SaveData
    {
        private static SaveData _instance;
        private SaveData() {}

        public static SaveData Instance => _instance ??= new SaveData();

        // Structures
        public List<CastleData> castleData = new();
        public List<BarracksData> barracksData = new();
        
        // Units        
        public List<BuilderData> builderData = new();
        public List<SoldierData> soldierData = new();
        public List<HorseData> horseData = new();
    }
}