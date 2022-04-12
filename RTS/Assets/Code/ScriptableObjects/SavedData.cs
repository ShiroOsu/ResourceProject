using Code.SaveSystem.Data;
using UnityEngine;

namespace Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SavedDataObject", menuName = "ScriptableObjects/SaveSystem/SavedData")]
    public class SavedData : ScriptableObject
    {
        public SaveData saveData;
    }
}