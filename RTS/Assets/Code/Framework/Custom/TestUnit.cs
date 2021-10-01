using System.IO;
using Code.Framework.Logger;
using UnityEditor;

namespace Code.Framework.Custom
{
    public static class TestUnit
    {
        private static TestUnitSO m_Unit;

        [MenuItem("Test/Create/Add Unit")]
        private static void CreateUnit()
        {
            Log.Message("TestUnit.cs", "Create Unit");
            SetUp();
            CreateFolderWithNewUnit();
        }

        private static void CreateFolderWithNewUnit()
        {
            if (Directory.Exists("Assets/Code/Units/" + m_Unit.UnitName) && EditorUtility.DisplayDialog("Unit exists",
                "Unit with the name " + m_Unit.UnitName + " already exists.", "Cancel"))
            {
                return;
            }

            var newFolder = AssetDatabase.CreateFolder("Assets/Code/Units", m_Unit.UnitName);
            var folderPath = AssetDatabase.GUIDToAssetPath(newFolder) + "/";
            var scriptFileName = m_Unit.UnitName + ".cs";
            var scriptFile = File.Create(folderPath + scriptFileName);
            scriptFile.Close();

            AssetDatabase.Refresh();
            AddPreCode(scriptFile.Name, m_Unit.UnitName);
        }

        private static void AddPreCode(string path, string fileName)
        {
            using (var outfile = new StreamWriter(File.Open(path, FileMode.Append)))
            {
                outfile.WriteLine("using Code.Framework.Interfaces;");
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine(" ");
                outfile.WriteLine("namespace Code.Units." + fileName);
                outfile.WriteLine("{");
                outfile.WriteLine("public class " + fileName + " : MonoBehaviour, IUnit");
                outfile.WriteLine("{");
                outfile.Write(m_Unit.preCode);                
            }
        }

        private static void SetUp()
        {
            m_Unit = (TestUnitSO)
                AssetDatabase.LoadAssetAtPath("Assets/Code/Framework/ScriptableObjects/TestUnitSO.asset",
                    typeof(TestUnitSO));
        }
    }
}