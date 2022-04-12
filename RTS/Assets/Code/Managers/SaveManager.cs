using System;
using System.Collections.Generic;
using Code.HelperClasses;
using Code.Interfaces;
using Code.SaveSystem;
using Code.SaveSystem.Data;
using UnityEngine;

namespace Code.Managers
{
    public class SaveManager : Singleton<SaveManager>
    {
        public GameObject savedGamesPanel;
        
        public event Action<SaveData, int> OnLoad;
        
        // Temp
        public List<GameObject> saves = new();

        private void OnEnable()
        {
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(savedGamesPanel);
        }

        public void Save(int index)
        {
            foreach (var obj in FindObjectsOfType<GameObject>(true))
            {
                if (obj.TryGetComponent(out ISavable savable) && obj.activeInHierarchy)
                {
                    savable.Save();
                }
            }
            
            
            SerializationManager.Save("SavedData", SaveData.Instance, index);
        }

        public void Load(int index)
        {
            var loadedData = (SaveData) SerializationManager.Load(Application.persistentDataPath + $"/saves/SavedData{index}.save");
            OnLoad?.Invoke(loadedData, index);
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