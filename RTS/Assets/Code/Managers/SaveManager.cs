using System;
using Code.HelperClasses;
using Code.Interfaces;
using Code.SaveSystem;
using Code.SaveSystem.Data;
using UnityEngine;

namespace Code.Managers
{
    public class SaveManager : Singleton<SaveManager>
    {
        public event Action<SaveData> SceneLoader;

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
            var loadedData = Extensions.LoadData<SaveData>($"/saves/SavedData{index}.save");
            SceneLoader?.Invoke(loadedData);
        }

        // Is this really needed?
        public Texture2D LoadImage(int saveFileIndex)
        {
            var loadedData = Extensions.LoadData<SaveData>($"/saves/SavedData{saveFileIndex}.save");

            if (loadedData == null)
            {
                return null;
            }

            var tempTexture2D = new Texture2D(1, 1);
            tempTexture2D.LoadImage(loadedData.imageInBytes);

            return tempTexture2D;
        }
    }
}