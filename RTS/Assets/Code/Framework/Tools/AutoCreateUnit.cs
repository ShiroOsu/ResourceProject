using System.IO;
#if UNITY_EDITOR
using Code.Framework.ExtensionFolder;
using Code.Framework.Logger;
using UnityEditor;
using UnityEngine;

namespace Code.Framework.Tools
{
    public static class AutoCreateUnit
    {
        private static AutoCreateScriptableObject _unit;
        public static string UnitName { get; set; }

        [MenuItem("Auto Create/Unit")]
        private static void CreateUnitInit()
        {
            Log.Message("AutoCreateUnit.cs", "Create Unit Initialization");
            AutoCreateWindowPopup.BeginCreation += CreateUnitBegin;
            InitializeAutoCreateWindowPopup();
        }

        private static void CreateUnitBegin()
        {
            SetUp();
            CreateUnitScript();
            CreateSo();
            CreateUnitManager();
            AddUnitEnum();
            AddToUIManager();
            AssetDatabase.Refresh();
            AutoCreateWindowPopup.BeginCreation -= CreateUnitBegin;
        }

        private static void CreateUnitScript()
        {
            Log.Message("AutoCreateUnit.cs", "Creating unit script");
            Log.Message("AutoCreateUnit.cs", "Creating unit prefab");
            var path = Application.dataPath + "/Code/Units/";

            AutoCreate.UnitFolder(UnitName);
            AutoCreate.UnitPrefab(UnitName);

            var scriptName = UnitName + ".cs";
            path += UnitName + "/";

            var scriptFile = File.Create(path + scriptName);
            scriptFile.Close();

            AutoCreate.UnitCode(scriptFile.Name, _unit.preCode, UnitName);
        }

        private static void InitializeAutoCreateWindowPopup()
        {
            AutoCreateWindowPopup.Init();
        }

        private static void CreateSo()
        {
            Log.Message("AutoCreateUnit.cs", "Creating Scriptable Object");

            var scriptName = UnitName + "SO.cs";
            var path = Application.dataPath + "/Code/Units/" + UnitName + "/" + scriptName;
            AutoCreate.UnitScriptableObject(path, UnitName);
        }

        private static void CreateUnitManager()
        {
            Log.Message("AutoCreateUnit.cs", "Creating UnitUIManager");

            var scriptName = UnitName + "UIManager.cs";
            var path = Application.dataPath + "/Code/Managers/Units/" + scriptName;
            AutoCreate.UnitManager(path, _unit.managerCode, UnitName);
        }

        private static void AddUnitEnum()
        {
            Log.Message("AutoCreateUnit.cs", "Creating Enum");

            var path = Application.dataPath + "/Code/Framework/Enums/Enums.cs";
            AutoCreate.UnitEnum(path, UnitName);
        }

        private static void AddToUIManager()
        {
            Log.Message("AutoCreateUnit.cs", "Adding new unit to UIManager");

            var path = Application.dataPath + "/Code/Managers/UIManager.cs";
            AutoCreate.UIManagerCode(path, UnitName);
        }

        private static void SetUp()
        {
            const string path =
                "Assets/Code/Framework/ScriptableObjects/AutoScriptableObjects/AutoCreateScriptableObject.asset";
            _unit = Extensions.LoadAsset<AutoCreateScriptableObject>(path);
        }
    }
}
#endif