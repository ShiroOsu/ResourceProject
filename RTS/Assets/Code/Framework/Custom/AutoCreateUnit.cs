using System.IO;
using Code.Framework.ExtensionFolder;
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
            SetUp();
            CreateUnitScript();
            CreateSO();
            CreateUnitManager();
            AddUnitEnum();
            AddToUIManager();
            AssetDatabase.Refresh();
            AutoCreateWindowPopup.BeginCreation -= CreateUnitBegin;
        }

        private static void CreateUnitScript()
        {
            Log.Message("AutoCreateUnit.cs", "Creating unit script");
            var path = Application.dataPath + "/Code/Units/";

            AutoCreate.UnitFolder(m_UnitName);
            AutoCreate.UnitPrefab(m_UnitName);

            var scriptName = m_UnitName + ".cs";
            path += m_UnitName + "/";

            var scriptFile = File.Create(path + scriptName);
            scriptFile.Close();

            AutoCreate.UnitCode(scriptFile.Name, m_Unit.preCode, m_UnitName);
        }

        private static void InitializeAutoCreateWindowPopup()
        {
            AutoCreateWindowPopup.Init();
        }

        private static void CreateSO()
        {
            Log.Message("AutoCreateUnit.cs", "Creating Scriptable Object");

            var scriptName = m_UnitName + "SO.cs";
            var path = Application.dataPath + "/Code/Units/" + m_UnitName + "/" + scriptName;
            AutoCreate.UnitScriptableObject(path, m_UnitName);
        }

        private static void CreateUnitManager()
        {
            Log.Message("AutoCreateUnit.cs", "Creating UnitUIManager");

            var scriptName = m_UnitName + "UIManager.cs";
            var path = Application.dataPath + "/Code/Managers/Units/" + scriptName;
            AutoCreate.UnitManager(path, m_Unit.managerCode, m_UnitName);
        }

        private static void AddUnitEnum()
        {
            Log.Message("AutoCreateUnit.cs", "Creating Enum");

            var path = Application.dataPath + "/Code/Framework/Enums/Enums.cs";
            AutoCreate.UnitEnum(path, m_UnitName);
        }

        private static void AddToUIManager()
        {
            Log.Message("AutoCreateUnit.cs", "Adding new unit to UIManager");

            var path = Application.dataPath + "/Code/Managers/UIManager.cs";
            AutoCreate.UIManagerCode(path, m_UnitName);
        }

        private static void SetUp()
        {
            const string path =
                "Assets/Code/Framework/ScriptableObjects/AutoScriptableObjects/AutoCreateScriptableObject.asset";
            m_Unit = Extensions.LoadAsset<AutoCreateScriptableObject>(path);
        }
    }
}