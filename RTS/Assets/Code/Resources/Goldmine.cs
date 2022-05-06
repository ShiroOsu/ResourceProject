using Code.Interfaces;
using Code.SaveSystem.Data;
using UnityEngine;

namespace Code.Resources
{
    public class Goldmine : MonoBehaviour, IResource, ISavable
    {
        public uint currentGoldLeft;
        public uint currentWorkersInMine;
        
        private const uint c_MaxNumberOfWorkers = 10;
        private const uint c_MaxAmountOfGold = 1000000;

        private readonly GoldmineData m_GoldmineData = new();

        private void Awake()
        {
            
        }

        public void Save()
        {
            m_GoldmineData.Save(gameObject, currentGoldLeft, currentWorkersInMine);
        }

        public void ShouldSelect(bool select)
        {
            //UIManager.Instance.ResourceSelected(select, gameObject, gameObject);
            
            //UIManager.Instance.StructureSelected(select, gameObject, StructureType.Castle, m_StructureImage, data);
            //castleTimer.Castle = this;
            //castleTimer.AddActionOnSpawn(select);
            //outlineRenderer.SetActive(select);

            if (!select)
            {
                //castleTimer.timer.transform.SetParent(transform);
            }
        }
    }
}
