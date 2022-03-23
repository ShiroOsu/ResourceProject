using System.IO;
using UnityEditor;

namespace Code.Tools.CreateSerializationSurrogate
{
    public static class CreateSerializationSurrogate
    {
        private const string c_PathToSurrogateTemplate = "Assets/Code/Tools/CreateSerializationSurrogate/SurrogateTemplate.cs";
        private const string c_Ss = "SerializationSurrogate";
        public static string Name { get; set; }
        
        [MenuItem("Assets/Tools/Create Serialization Surrogate", false, 1)]
        public static void CreateSurrogate()
        {
            if (Selection.activeObject is null) return;
            
            NamingWindowPopup.SetName += StartCreate;
            SetNameInit();
        }

        private static void StartCreate()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
            }

            if (string.IsNullOrEmpty(path))
                path = "Assets/";

            string newString;
            using (var sr = File.OpenText(c_PathToSurrogateTemplate))
            {
                var txt = sr.ReadToEnd();
                var tempText = txt;
                tempText = tempText.Replace("name", Name + c_Ss);
                newString = tempText;
            }
            
            using (var outfile = new StreamWriter(File.Create(path + "/" + Name + c_Ss + ".cs")))
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