using System;
using Code.Enums;
using Code.HelperClasses;
using Code.Interfaces;
using Code.Managers.UI;
using Code.SaveSystem.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Units.Horse
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class HorseUnit : MonoBehaviour, IUnit, ISavable
    {
        [SerializeField] private GameObject selectionCircle;
        [SerializeField] private UnitData data;
        private GameObject m_UnitImage;
        private NavMeshAgent m_Agent;
        
        private readonly HorseData m_HorseData = new();

        private void Awake()
        {
            m_Agent = GetComponent<NavMeshAgent>();

            m_Agent.speed = data.movementSpeed;
            m_Agent.acceleration = data.acceleration;
            
            if (!m_UnitImage)
            {
                m_UnitImage = Extensions.FindObject("HorseImage");
            }
        }

        public void ShouldSelect(bool select)
        {
            UIManager.Instance.UnitSelected(select, gameObject, UnitType.Horse, m_UnitImage, data);
            ActivateSelectionCircle(select);
        }

        public void ActivateSelectionCircle(bool active)
        {
            selectionCircle.SetActive(active);
        }

        public void OnDestroy()
        {
            Destroy(this);
        }

        public UnitType GetUnitType()
        {
            return UnitType.Horse;
        }
        
        public TextureAssetType GetUnitTexture()
        {
            return TextureAssetType.Horse;
        }
        
        public UnitData GetUnitData()
        {
            return data;
        }

        public GameObject GetUnitImage()
        {
            return m_UnitImage;
        }
        public GameObject GetUnitObject()
        {
            return gameObject;
        }

        public void Move(Vector3 destination)
        {
            m_Agent.SetDestination(destination);
        }

        public bool IsUnitMoving()
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            m_HorseData.Save(gameObject);
            SaveData.Instance.horseData.Add(m_HorseData);
        }
    }
}
