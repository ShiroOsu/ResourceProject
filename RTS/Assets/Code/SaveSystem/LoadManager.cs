using Code.Managers;
using Code.SaveSystem.Data;
using UnityEngine;

namespace Code.SaveSystem
{
    public class LoadManager : MonoBehaviour
    {
        private void Start()
        {
            SaveManager.Instance.OnLoad += LoadSaveData;
        }

        private void LoadSaveData(SaveData sd, int saveIndex)
        {
            
        }
    }
}
