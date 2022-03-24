using System;
using Code.HelperClasses;
using Code.SaveSystem;
using Code.SaveSystem.Data;
using UnityEngine;

namespace Code.Managers
{
    public class SaveManager : Singleton<SaveManager>
    {
        public event Action<SaveData> OnLoad;
        public event Action OnSave;
    
        public void Save()
        {
            OnSave?.Invoke();
            SerializationManager.Save("SavedData", SaveData.Instance);
        }

        public void Load()
        {
            var loadedData = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/SavedData.save");
            OnLoad?.Invoke(loadedData);
        }
    }
}