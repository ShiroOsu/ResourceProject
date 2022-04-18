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
        public event Action<SaveData, int> OnLoad;

        public void Save(int index, byte[] imageInBytes)
        {
            foreach (var obj in FindObjectsOfType<GameObject>(true))
            {
                if (obj.TryGetComponent(out ISavable savable) && obj.activeInHierarchy)
                {
                    savable.Save();
                }
            }

            SaveData.Instance.imageInBytes = new byte[imageInBytes.Length];
            SaveData.Instance.imageInBytes = imageInBytes;
            SerializationManager.Save("SavedData", SaveData.Instance, index);
        }

        public void Load(int index)
        {
            var loadedData = (SaveData) SerializationManager.Load(Application.persistentDataPath + $"/saves/SavedData{index}.save");
            OnLoad?.Invoke(loadedData, index);
        }

        public Texture2D LoadImage(int saveFileIndex)
        {
            var loadedData = (SaveData)SerializationManager.Load(Application.persistentDataPath + $"/saves/SavedData{saveFileIndex}.save");

            if (loadedData.imageInBytes == null)
            {
                return null;
            }

            var tempTexture2D = new Texture2D(1, 1);
            tempTexture2D.LoadImage(loadedData.imageInBytes);

            return tempTexture2D;
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