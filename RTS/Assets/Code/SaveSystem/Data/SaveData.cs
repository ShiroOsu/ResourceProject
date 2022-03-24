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
        public List<CastleData> castleData;
        public List<BarracksData> barracksData;
        
        // Units        
        public List<BuilderData> builderData;
        public List<SoldierData> soldierData;
        public List<HorseData> horseData;

    }
}