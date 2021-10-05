using System.IO;
using NUnit.Framework.Internal.Filters;

namespace Code.Framework.Extensions
{
    public static class AutoCreate
    {
        public static void UnitScript(string path, string preCode, string unitName)
        {
            using (var outfile = new StreamWriter(File.Open(path, FileMode.Append)))
            {
                outfile.WriteLine("using Code.Framework.Interfaces;");
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
                outfile.WriteLine("[CreateAssetMenu(fileName = \"" + unitSO + "\", menuName = " + "\"ScriptableObjects/" + unitName + "\"" + ")]");
                outfile.WriteLine("public class " + unitSO + " : ScriptableObject");
                outfile.WriteLine("{");
                outfile.WriteLine("}");
                outfile.WriteLine("}");
            }
        }

        public static void UnitManager(string path, string unitName, string managerPreCode)
        {
            using (var outfile = new StreamWriter(File.Create(path)))
            {
                outfile.WriteLine("using Code.Units;");
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine(" ");
                outfile.WriteLine("namespace Code.Managers.Units");
                outfile.WriteLine("{");
                outfile.WriteLine("public class " + unitName + " : MonoBehaviour");
                outfile.Write(managerPreCode);
            }
        }
        
        // Not complete yet
        public static void UnitEnum(string path, string unitName)
        {
            var newString = "";
            
            using (var sr = File.OpenText(path))
            {
                string text = sr.ReadToEnd();
                int i = text.IndexOf('}');
                newString = text.Insert(i, $"\t{unitName + ","}\n\t");
            }
            
            using (var outfile = new StreamWriter(File.Open(path, FileMode.Create)))
            {
                outfile.Write(newString);
            }
        }
    }
}