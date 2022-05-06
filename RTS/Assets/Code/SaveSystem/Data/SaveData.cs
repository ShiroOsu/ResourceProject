using System.Collections.Generic;

namespace Code.SaveSystem.Data
{
    [System.Serializable]
    public class SaveData
    {
        private static SaveData _instance;
        private SaveData() {}

        public static SaveData Instance => _instance ??= new SaveData();

        // Save-file Image
        public byte[] imageInBytes;

        // Structures
        public List<CastleData> castleData = new();
        public List<BarracksData> barracksData = new();
        
        // Units        
        public List<BuilderData> builderData = new();
        public List<SoldierData> soldierData = new();
        public List<HorseData> horseData = new();
        
        // Resources
        public List<GoldmineData> goldminesData = new();

        public void OnDestroy()
        {
            imageInBytes = null;
            castleData = null;
            barracksData = null;
            builderData = null;
            soldierData = null;
            horseData = null;
            goldminesData = null;
        }

    }
}