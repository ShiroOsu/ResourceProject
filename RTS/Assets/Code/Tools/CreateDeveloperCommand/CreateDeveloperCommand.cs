using System.IO;
using UnityEditor;
using UnityEngine;

namespace Code.Tools.CreateDeveloperCommand
{
    public static class CreateDeveloperCommand
    {
        private const string c_PathToCommandTemplate = "Assets/Code/Tools/CreateDeveloperCommand/CommandTemplate.cs";
        private const string c_PathToScriptableObjectsFolder = "Assets/ScriptableObjects/Commands/";
        private const string c_Cs = ".cs";

        [MenuItem("Assets/Tools/Create Developer Command", false, 3)]
        public static void Create()
        {
            if (Selection.activeObject is null) return;

            NamingWindowPopup.SetName += StartCreate;
            Init();
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
            using (var sr = File.OpenText(c_PathToCommandTemplate))
            {
                var txt = sr.ReadToEnd();
                var tempTxt = txt;
                tempTxt = tempTxt.Replace("Temp", name);
                tempTxt = tempTxt.Replace("//", "");
                newString = tempTxt;
            }

            using (var outfile = new StreamWriter(File.Create(path + "/" + name + "Command" + c_Cs)))
            {
                outfile.Write(newString);
            }

            var scriptName = name + "Command";
            NamingWindowPopup.SetName -= StartCreate;
            
            IsCompiling.Init(scriptName);
            AssetDatabase.Refresh();
            EditorUtility.RequestScriptReload();
        }

        public static void CreateScriptableObject(string commandName)
        {
            var newAsset = ScriptableObject.CreateInstance(commandName);
            AssetDatabase.CreateAsset(newAsset, c_PathToScriptableObjectsFolder + commandName + ".asset");
        }

        private static void Init()
        {
            NamingWindowPopup.Init();
        }
    }
}