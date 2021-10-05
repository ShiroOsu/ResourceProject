using System.IO;
using Code.Framework.Extensions;
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
            InitializeAutoCreateWindowPopup();
        }

        private static void CreateUnitBegin()
        {
            Log.Message("AutoCreateUnit.cs", "Create " + m_UnitName + " Unit Begin");
            SetUp();
            CreateFolderWithNewUnit();
            CreateSO();
            CreateUnitManager();
            AddUnitEnum();
            AssetDatabase.Refresh();
            AutoCreateWindowPopup.BeginCreation -= CreateUnitBegin;
        }

        private static void CreateFolderWithNewUnit()
        {
            if (Directory.Exists(Application.dataPath + "/Code/Units/" + m_UnitName) && EditorUtility.DisplayDialog("Unit exists",
                "Unit with the name " + m_UnitName + " already exists.", "Cancel"))
            {
                Log.Error("AutoCreateUnit", "Stopping Creation");
                return;
            }

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
            AutoCreate.UnitScript(path, m_Unit.preCode, fileName);
        }

        private static void InitializeAutoCreateWindowPopup()
        {
            AutoCreateWindowPopup.Init();
        }

        private static void CreateSO()
        {
            Log.Message("AutoCreateUnit", "Creating Scriptable Object");
            
            var path = Application.dataPath + "/Code/Units/" + m_UnitName + "/" + m_UnitName + "SO.cs";
            AutoCreate.UnitScriptableObject(path, m_UnitName);
        }

        private static void CreateUnitManager()
        {
            var path = Application.dataPath + "/Code/Managers/Units/" + m_UnitName + "UIManager.cs";
            AutoCreate.UnitManager(path, m_UnitName, m_Unit.managerCode);
        }

        private static void AddUnitEnum()
        {
            var path = Application.dataPath + "/Code/Framework/Enums/Enums.cs";
            AutoCreate.UnitEnum(path, m_UnitName);
        }

        private static void SetUp()
        {
            const string path = "Assets/Code/Framework/ScriptableObjects/AutoScriptableObjects/AutoCreateScriptableObject.asset";
            m_Unit = (AutoCreateScriptableObject)Extensions.Extensions.LoadAsset<AutoCreateScriptableObject>(path);
        }
    }
}