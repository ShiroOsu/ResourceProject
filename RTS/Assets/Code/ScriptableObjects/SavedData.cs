using System.Collections.Generic;
using Code.SaveSystem.Data;
using UnityEngine;

namespace Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SavedDataObject", menuName = "ScriptableObjects/SaveSystem/SavedData")]
    public class SavedData : ScriptableObject
    {
        public List<CastleData> castleDataList;
        public List<BarracksData> barrackDataList;
    }
}