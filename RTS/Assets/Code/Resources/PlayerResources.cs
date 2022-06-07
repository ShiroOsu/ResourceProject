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
        private int m_Units;

        public void AddResource(int gold = 0, int stone = 0, int wood = 0, int food = 0, int units = 0)
        {
            m_FirstLoad = false;
            
            m_Gold += gold;
            m_Stone += stone;
            m_Wood += wood;
            m_Food += food;
            m_Units += units;
            
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
                ResourceType.Units => m_Units.ToString(),
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
            m_Units = 0;
            UpdateResourcePanel();
        }

        private void UpdateResourcePanel()
        {
            OnUpdateResourcePanel?.Invoke();
        }

        public void Save()
        {
            m_ResourceData.Save(m_Gold, m_Stone, m_Wood, m_Food, m_Units);
            SaveData.Instance.playerResourceData = m_ResourceData;
        }
    }
}