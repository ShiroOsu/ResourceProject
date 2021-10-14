using System;
using System.IO;
using UnityEditor;

namespace Code.Framework.Extensions
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
                var unitSO = unitName + "SO";

                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine(" ");
                outfile.WriteLine("namespace Code.Units." + unitName);
                outfile.WriteLine("{");
                outfile.WriteLine("[CreateAssetMenu(fileName = \"" + unitSO + "\", menuName = " +
                                  "\"ScriptableObjects/" + unitName + "\"" + ")]");
                outfile.WriteLine("public class " + unitSO + " : ScriptableObject");
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

        public static void UnitFolder(string path, string unitName)
        {
            if (Directory.Exists(path + unitName) && EditorUtility.DisplayDialog("Unit exists",
                "Unit with the name " + unitName + " already exists.", "Cancel"))
            {
                return;
            }

            AssetDatabase.CreateFolder("Assets/Code/Units", unitName);
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
            UnitType = 0,
            TextureAssetType = 4,
        }

        #endregion


        private static string InsertAfterHeader(string text, string stringToInsert)
        {
            int index = text.IndexOf(']');
            text = text.Insert(index + 1, $"\n\t\t{stringToInsert}");
            return text;
        }

        public static void UIManagerCode(string path, string unitName)
        {
            var newString = "";
            var insertString = "[SerializeField] private "+ unitName +"UIManager " + "m_" + unitName + ";";

            using (var sr = File.OpenText(path))
            {
                string text = sr.ReadToEnd();
                newString = InsertAfterHeader(text, insertString);
            }

            using (var outfile = new StreamWriter(File.Open(path, FileMode.Create)))
            {
                outfile.Write(newString);
            }
        }
    }
}