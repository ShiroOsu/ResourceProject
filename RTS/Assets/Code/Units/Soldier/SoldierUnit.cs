using Code.SaveSystem.Data;
using Code.Tools.Enums;
using Code.Tools.HelperClasses;
using Code.UI;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Units.Soldier
{
    public class SoldierUnit : BaseUnit
    {
        [SerializeField] private GameObject selectionCircle;
        [SerializeField] private UnitData data;
        private GameObject m_UnitImage;
        private readonly SoldierData m_SoldierData = new();

        private void Awake()
        {
            UnitType = UnitType.Soldier;
            TextureAssetType = TextureAssetType.Soldier;
            
            Agent = GetComponent<NavMeshAgent>();
            Agent.speed = data.movementSpeed;
            Agent.acceleration = data.acceleration;
            
            if (!m_UnitImage)
            {
                m_UnitImage = Extensions.FindObject("SoldierImage");
            }
        }
        
        public override GameObject GetUnitImage()
        {
            return m_UnitImage;
        }

        public override void ShouldSelect(bool select)
        {
            UIManager.Instance.UnitSelected(select, gameObject, UnitType.Soldier, m_UnitImage, data);
            ActivateSelectionCircle(select);
        }
        
        public override void ActivateSelectionCircle(bool active)
        {
            selectionCircle.SetActive(active);
        }

        public override void Save()
        {
            m_SoldierData.Save(gameObject);
            SaveData.Instance.soldierData.Add(m_SoldierData);
        }
    }
}