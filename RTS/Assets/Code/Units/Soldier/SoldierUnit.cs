using System;
using Code.Enums;
using Code.HelperClasses;
using Code.Interfaces;
using Code.Managers.UI;
using Code.SaveSystem.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Units.Soldier
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SoldierUnit : MonoBehaviour, IUnit, ISavable
    {
        [SerializeField] private GameObject selectionCircle;
        [SerializeField] private UnitData data;
        private GameObject m_UnitImage;
        private NavMeshAgent m_Agent;
        private SoldierData m_SoldierData;
        private Guid m_DataID;
        
        private void OnEnable()
        {
            // TODO: Store
            if (m_SoldierData is null)
            {
                m_SoldierData = new(Guid.NewGuid());
                m_DataID = m_SoldierData.dataID;
            }
        }

        private void Awake()
        {
            m_Agent = GetComponent<NavMeshAgent>();

            m_Agent.speed = data.movementSpeed;
            m_Agent.acceleration = data.acceleration;
            
            if (!m_UnitImage)
            {
                m_UnitImage = Extensions.FindObject("SoldierImage");
            }
        }

        public UnitType GetUnitType()
        {
            return UnitType.Soldier;
        }
        
        public TextureAssetType GetUnitTexture()
        {
            return TextureAssetType.Soldier;
        }
        
        public UnitData GetUnitData()
        {
            return data;
        }

        public GameObject GetUnitImage()
        {
            return m_UnitImage;
        }

        public void ShouldSelect(bool select)
        {
            UIManager.Instance.UnitSelected(select, gameObject, UnitType.Soldier, m_UnitImage, data);
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

        public void Move(Vector3 destination)
        {
            m_Agent.SetDestination(destination);
        }

        public bool IsUnitMoving()
        {
            throw new System.NotImplementedException();
        }

        public Vector3Int GetPosition()
        {
            return gameObject.transform.position.Vector3ToVector3Int();
        }

        public void Save()
        {
            m_SoldierData.Save(gameObject);
            SaveData.Instance.soldierData.Add(m_SoldierData);
        }
    }
}
