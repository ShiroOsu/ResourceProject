namespace Code.SaveSystem
{
    public class SaveData
    {
        private static SaveData _instance;
        private SaveData() {}

        public static SaveData Instance => _instance ??= new SaveData();
    }
}