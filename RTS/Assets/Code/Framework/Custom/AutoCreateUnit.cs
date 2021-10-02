using System;
using System.IO;
using Code.Framework.Logger;
using UnityEditor;
using UnityEngine;

namespace Code.Framework.Custom
{
    public static class AutoCreateUnit
    {
        private static AutoCreateScriptableObject m_Unit;
        public static string m_UnitName { get; set; }


        [MenuItem("Auto Create/Unit")]
        private static void CreateUnitInit()
        {
            Log.Message("AutoCreateUnit.cs", "Create Unit Initialization");
            AutoCreateWindowPopup.BeginCreation += CreateUnitBegin;
            SOSetUp();
        }

        private static void CreateUnitBegin()
        {
            Log.Message("AutoCreateUnit.cs", "Create " + m_UnitName + " Unit Begin");
            SetUp();
            CreateFolderWithNewUnit();
            CreateSO();
            AssetDatabase.Refresh();
            AutoCreateWindowPopup.BeginCreation -= CreateUnitBegin;
        }

        private static void CreateFolderWithNewUnit()
        {
            if (Directory.Exists("Assets/Code/Units/" + m_UnitName) && EditorUtility.DisplayDialog("Unit exists",
                "Unit with the name " + m_UnitName + " already exists.", "Cancel"))
            {
                Log.Error("AutoCreateUnit", "Stopping Creation");
                return;
            }

            Log.Message("AutoCreateUnit.cs", "Creating Folder and Script file");

            var newFolder = AssetDatabase.CreateFolder("Assets/Code/Units", m_UnitName);
            var folderPath = AssetDatabase.GUIDToAssetPath(newFolder) + "/";
            var scriptFileName = m_UnitName + ".cs";
            var scriptFile = File.Create(folderPath + scriptFileName);
            scriptFile.Close();

            AddPreCode(scriptFile.Name, m_UnitName);
        }

        private static void AddPreCode(string path, string fileName)
        {
            Log.Message("AutoCreateUnit.cs", "Writing to script file");

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

        private static void SOSetUp()
        {
            Log.Message("AutoCreateUnit.cs", "Window Popup");
            AutoCreateWindowPopup.Init();

            if (!Directory.Exists("Assets/Code/Framework/ScriptableObjects/AutoScriptableObjects"))
            {
                AssetDatabase.CreateFolder("Assets/Code/Framework/ScriptableObjects", "AutoScriptableObjects");
            }
        }

        private static void CreateSO()
        {
            Log.Message("AutoCreateUnit", "Creating SO");
            
            var folderPath = Application.dataPath + "/Code/Units/" + m_UnitName + "/";
            var scriptFileName = m_UnitName + "SO.cs";
            var scriptFile = File.Create(folderPath + scriptFileName);
            scriptFile.Close();

            using (var outfile = new StreamWriter(File.Open(scriptFile.Name, FileMode.Append)))
            {
                var unitName = m_UnitName + "SO";
                
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine(" ");
                outfile.WriteLine("namespace Code.Units." + m_UnitName);
                outfile.WriteLine("{");
                outfile.WriteLine("[CreateAssetMenu(fileName = \"" + unitName + "\", menuName = " + "\"ScriptableObjects/" + unitName + "\"" + ")]");
                outfile.WriteLine("public class " + unitName + " : ScriptableObject");
                outfile.WriteLine("{");
                outfile.WriteLine("}");
                outfile.WriteLine("}");
            }
        }

        private static void SetUp()
        {
            m_Unit = (AutoCreateScriptableObject)
                AssetDatabase.LoadAssetAtPath(
                    "Assets/Code/Framework/ScriptableObjects/AutoScriptableObjects/AutoCreateScriptableObject.asset",
                    typeof(AutoCreateScriptableObject));
        }
    }
}