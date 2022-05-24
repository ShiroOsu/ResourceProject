using Code.HelperClasses;
using Code.Interfaces;
using Code.SaveSystem.Data;
using Code.ScriptableObjects;
using UnityEngine;

namespace Code.Resources
{
    public class PlayerResources : Singleton<PlayerResources>, ISavable
    {
        [SerializeField] private PlayerStartResources startResources;
        private PlayerResourceData m_ResourceData;
        
        private int m_Gold;
        private int m_Stone;
        private int m_Wood;
        private int m_Food;

        public void AddResource(int gold = 0, int stone = 0, int wood = 0, int food = 0)
        {
            m_Gold = gold;
            m_Stone = stone;
            m_Wood = wood;
            m_Food = food;
        }

        public void Save()
        {
            m_ResourceData.Save(m_Gold, m_Stone, m_Wood, m_Food);
            SaveData.Instance.playerResourceData = m_ResourceData;
        }
    }
}