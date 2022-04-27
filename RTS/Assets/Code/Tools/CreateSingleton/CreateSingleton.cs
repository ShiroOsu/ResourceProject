using System.IO;
using UnityEditor;

namespace Code.Tools.CreateSingleton
{
    public static class CreateSingleton
    {
        private const string c_PathToSingletonTemplate = "Assets/Code/Tools/CreateSingleton/SingletonTemplate.cs";
        private const string c_Cs = ".cs";
        
        [MenuItem("Assets/Tools/Create Singleton", false, 2)]
        public static void Create()
        {
            if (Selection.activeObject is null) return;
            
            NamingWindowPopup.SetName += StartCreate;
            SetNameInit();
        }

        private static void StartCreate(string name)
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
            }

            if (string.IsNullOrEmpty(path))
                path = "Assets/";

            string newString;
            using (var sr = File.OpenText(c_PathToSingletonTemplate))
            {
                var txt = sr.ReadToEnd();
                var tempText = txt;
                tempText = tempText.Replace("Name", name);
                newString = tempText;
            }
            
            using (var outfile = new StreamWriter(File.Create(path + "/" + name + c_Cs)))
            {
                outfile.Write(newString);
            }

            NamingWindowPopup.SetName -= StartCreate;
            AssetDatabase.Refresh();
        }
        
        private static void SetNameInit()
        {
            NamingWindowPopup.Init();
        }
    }
}