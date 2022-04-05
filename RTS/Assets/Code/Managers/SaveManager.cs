using System;
using System.Collections.Generic;
using Code.HelperClasses;
using Code.SaveSystem;
using Code.SaveSystem.Data;
using Code.ScriptableObjects;
using UnityEngine;

namespace Code.Managers
{
    public class SaveManager : Singleton<SaveManager>
    {
        public event Action<SaveData> OnLoad;
        public event Action OnSave;
        public SavedData savedDataObject;

        public void Save()
        {
            OnSave?.Invoke();
            SaveToSavedDataObject(SaveData.Instance);
            SerializationManager.Save("SavedData", SaveData.Instance);
        }

        public void Load()
        {
            var loadedData = (SaveData) SerializationManager.Load(Application.persistentDataPath + "/saves/SavedData.save");
            OnLoad?.Invoke(loadedData);
        }

        private void SaveToSavedDataObject(SaveData sd)
        {
            CheckIfObjectExistsAndOverride(savedDataObject.castleDataList, sd.castleData);
            CheckIfObjectExistsAndOverride(savedDataObject.barrackDataList, sd.barracksData);
        }

        private static void CheckIfObjectExistsAndOverride<T>(List<T> oldSavedList, List<T> newSaveList) where T : BaseData
        {
            if (oldSavedList.Count < 1)
            {
                oldSavedList.AddRange(newSaveList);
                return;
            }

            foreach (var save in newSaveList)
            {
                for (var i = 0; i < oldSavedList.Count; i++)
                {
                    if (oldSavedList[i].dataID == save.dataID)
                    {
                        oldSavedList[i] = save;
                    }
                    else
                    {
                        oldSavedList.Add(save);
                    }
                }
            }
        }
    }
}