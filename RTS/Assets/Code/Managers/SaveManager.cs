using System;
using System.Collections.Generic;
using Code.HelperClasses;
using Code.Interfaces;
using Code.SaveSystem;
using Code.SaveSystem.Data;
using Code.ScriptableObjects;
using UnityEngine;

namespace Code.Managers
{
    public class SaveManager : Singleton<SaveManager>
    {
        public event Action<SaveData, int> OnLoad;
        public SavedData[] savedDataObject;

        private void OnEnable()
        {
            DontDestroyOnLoad(this);
        }

        private void Save(int index)
        {
            foreach (var obj in FindObjectsOfType<GameObject>(true))
            {
                if (obj.TryGetComponent(out ISavable savable))
                {
                    savable.Save();
                }
            }
            
            SaveToSavedDataObject(SaveData.Instance, index);
            SerializationManager.Save("SavedData", SaveData.Instance);
        }

        public void Save()
        {
            
        }

        public void Load(int index)
        {
            var loadedData = (SaveData) SerializationManager.Load(Application.persistentDataPath + "/saves/SavedData.save");
            OnLoad?.Invoke(loadedData, index);
        }

        private void SaveToSavedDataObject(SaveData sd, int index)
        {
            CheckIfObjectExistsAndOverride(savedDataObject[index].castleDataList, sd.castleData);
            CheckIfObjectExistsAndOverride(savedDataObject[index].barrackDataList, sd.barracksData);
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