using System;
using Code.Enums;
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
        private readonly PlayerResourceData m_ResourceData = new();
        public event Action OnUpdateResourcePanel;
        private bool m_FirstLoad = true;
        
        private int m_Gold;
        private int m_Stone;
        private int m_Wood;
        private int m_Food;

        public void AddResource(int gold = 0, int stone = 0, int wood = 0, int food = 0)
        {
            m_FirstLoad = false;
            
            m_Gold += gold;
            m_Stone += stone;
            m_Wood += wood;
            m_Food += food;
            
            UpdateResourcePanel();
        }

        // GetResource In string
        public string GetResource(ResourceType type)
        {
            return type switch
            {
                ResourceType.Gold => m_Gold.ToString(),
                ResourceType.Stone => m_Stone.ToString(),
                ResourceType.Wood => m_Wood.ToString(),
                ResourceType.Food => m_Food.ToString(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public void SetDataValues()
        {
            if (!m_FirstLoad) return;

            m_Gold = startResources.gold;
            m_Stone = startResources.stone;
            m_Wood = startResources.wood;
            m_Food = startResources.food;
            UpdateResourcePanel();
        }

        private void UpdateResourcePanel()
        {
            OnUpdateResourcePanel?.Invoke();
        }

        public void Save()
        {
            m_ResourceData.Save(m_Gold, m_Stone, m_Wood, m_Food);
            SaveData.Instance.playerResourceData = m_ResourceData;
        }
    }
}