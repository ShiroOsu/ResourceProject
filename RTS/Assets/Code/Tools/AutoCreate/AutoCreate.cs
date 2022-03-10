#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Tools.AutoCreate
{
    public static class AutoCreate
    {
        public static void UnitCode(string path, string preCode, string unitName)
        {
            using (var outfile = new StreamWriter(File.Open(path, FileMode.Append)))
            {
                outfile.WriteLine("using Code.Framework.Interfaces;");
                outfile.WriteLine("using Code.Framework.Enums;");
                outfile.WriteLine("using Code.Managers;");
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine(" ");
                outfile.WriteLine("namespace Code.Units." + unitName);
                outfile.WriteLine("{");
                outfile.WriteLine("public class " + unitName + " : MonoBehaviour, IUnit");
                outfile.WriteLine("{");
                outfile.Write(preCode);
            }
        }

        public static void UnitScriptableObject(string path, string unitName)
        {
            using (var outfile = new StreamWriter(File.Create(path)))
            {
                var unitSo = unitName + "SO";

                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine(" ");
                outfile.WriteLine("namespace Code.Units." + unitName);
                outfile.WriteLine("{");
                outfile.WriteLine("[CreateAssetMenu(fileName = \"" + unitSo + "\", menuName = " +
                                  "\"ScriptableObjects/" + unitName + "\"" + ")]");
                outfile.WriteLine("public class " + unitSo + " : ScriptableObject");
                outfile.WriteLine("{");
                outfile.WriteLine("}");
                outfile.WriteLine("}");
            }
        }

        public static void UnitManager(string path, string managerPreCode, string unitName)
        {
            using (var outfile = new StreamWriter(File.Create(path)))
            {
                outfile.WriteLine("using Code.Units;");
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine(" ");
                outfile.WriteLine("namespace Code.Managers.Units");
                outfile.WriteLine("{");
                outfile.WriteLine("public class " + unitName + "UIManager " + ": MonoBehaviour");
                outfile.Write(managerPreCode);
            }
        }

        public static void UnitFolder(string unitName)
        {
            if (Directory.Exists("Assets/Code/Units/" + unitName) && EditorUtility.DisplayDialog("Unit exists",
                "Unit with the name " + unitName + " already exists.", "Cancel"))
            {
                return;
            }

            AssetDatabase.CreateFolder("Assets/Code/Units", unitName);
        }

        public static void UnitPrefab(string unitName)
        {
            if (Directory.Exists("Assets/Prefabs/Units/" + unitName) && EditorUtility.DisplayDialog(
                unitName + " prefab folder exists", "", "Ok"))
            {
                return;
            }
            
            AssetDatabase.CreateFolder("Assets/Prefabs/Units", unitName);
            string path = "Assets/Prefabs/Units/" + unitName + ".prefab";
            var newObj = new GameObject {name = unitName};
            PrefabUtility.SaveAsPrefabAsset(newObj, path);
            var asset = AssetDatabase.FindAssets($"{unitName}", new []{"Assets/Prefabs/Units"});
            var oldPath = AssetDatabase.GUIDToAssetPath(asset[1]);
            AssetDatabase.MoveAsset(oldPath, $"Assets/Prefabs/Units/{unitName}/{unitName}.prefab");
            Object.DestroyImmediate(GameObject.Find($"{unitName}"));
        }

        #region AddEnum

        public static void UnitEnum(string path, string unitName)
        {
            var newString = "";

            using (var sr = File.OpenText(path))
            {
                string text = sr.ReadToEnd();
                newString = InsertNewEnum(text, unitName);
            }

            using (var outfile = new StreamWriter(File.Open(path, FileMode.Create)))
            {
                outfile.Write(newString);
            }
        }

        private static string InsertNewEnum(string text, string unitName)
        {
            foreach (var type in Enum.GetNames(typeof(EnumTypeIndex)))
            {
                int index = text.IndexOf('}', text.IndexOf(type));
                text = text.Insert(index, $"\t{unitName},\n\t");
            }

            return text;
        }

        private enum EnumTypeIndex
        {
            TextureAssetType = 3,
        }

        #endregion


        private static string InsertAfterHeader(string text, string insertString)
        {
            int index = text.IndexOf(']');
            text = text.Insert(index + 1, $"\n\t\t{insertString}");
            return text;
        }
        
        private static string InsertUnitToSwitch(string text, string insertString)
        {
            int index = text.IndexOf('{', text.IndexOf("type"));
            text = text.Insert(index + 1, $"\n\t\t\t\t{insertString}");
            return text;
        }

        public static void UIManagerCode(string path, string unitName)
        {
            var newString = "";
            var insertString = "[SerializeField] private "+ unitName +"UIManager " + "m_" + unitName + ";";
            
            var str = "case TextureAssetType." + unitName + ":" + "\n\t\t\t\t\tm_" + unitName 
                      + ".EnableMainUI(select, unit);" + "\n\t\t\t\t\tbreak;";

            using (var sr = File.OpenText(path))
            {
                string text = sr.ReadToEnd();
                var tempStr = InsertAfterHeader(text, insertString);
                newString = InsertUnitToSwitch(tempStr, str);
            }

            using (var outfile = new StreamWriter(File.Open(path, FileMode.Create)))
            {
                outfile.Write(newString);
            }
        }
    }
}
#endif