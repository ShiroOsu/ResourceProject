#if UNITY_EDITOR
using System.IO;
using Code.Debugging;
using Code.HelperClasses;
using UnityEditor;
using UnityEngine;

namespace Code.Tools.AutoCreate
{
    public static class AutoCreateUnit
    {
        private static AutoCreateScriptableObject _unit;
        public static string UnitName { get; set; }

        [MenuItem("Auto Create/Unit")]
        private static void CreateUnitInit()
        {
            Log.Print("AutoCreateUnit.cs", "Code is probably broken since last folder restructure.");
            return;
            
#pragma warning disable CS0162
            Log.Print("AutoCreateUnit.cs", "Create Unit Initialization");
            AutoCreateWindowPopup.BeginCreation += CreateUnitBegin;
            InitializeAutoCreateWindowPopup();
#pragma warning restore CS0162
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
            Log.Print("AutoCreateUnit.cs", "Creating unit script");
            Log.Print("AutoCreateUnit.cs", "Creating unit prefab");
            var path = Application.dataPath + "/Code/Units/";

            Tools.AutoCreate.AutoCreate.UnitFolder(UnitName);
            Tools.AutoCreate.AutoCreate.UnitPrefab(UnitName);

            var scriptName = UnitName + ".cs";
            path += UnitName + "/";

            var scriptFile = File.Create(path + scriptName);
            scriptFile.Close();

            Tools.AutoCreate.AutoCreate.UnitCode(scriptFile.Name, _unit.preCode, UnitName);
        }

        private static void InitializeAutoCreateWindowPopup()
        {
            AutoCreateWindowPopup.Init();
        }

        private static void CreateSo()
        {
            Log.Print("AutoCreateUnit.cs", "Creating Scriptable Object");

            var scriptName = UnitName + "SO.cs";
            var path = Application.dataPath + "/Code/Units/" + UnitName + "/" + scriptName;
            Tools.AutoCreate.AutoCreate.UnitScriptableObject(path, UnitName);
        }

        private static void CreateUnitManager()
        {
            Log.Print("AutoCreateUnit.cs", "Creating UnitUIManager");

            var scriptName = UnitName + "UIManager.cs";
            var path = Application.dataPath + "/Code/Managers/Units/" + scriptName;
            Tools.AutoCreate.AutoCreate.UnitManager(path, _unit.managerCode, UnitName);
        }

        private static void AddUnitEnum()
        {
            Log.Print("AutoCreateUnit.cs", "Creating Enum");

            var path = Application.dataPath + "/Code/Framework/Enums/Enums.cs";
            Tools.AutoCreate.AutoCreate.UnitEnum(path, UnitName);
        }

        private static void AddToUIManager()
        {
            Log.Print("AutoCreateUnit.cs", "Adding new unit to UIManager");

            var path = Application.dataPath + "/Code/Managers/UIManager.cs";
            Tools.AutoCreate.AutoCreate.UIManagerCode(path, UnitName);
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