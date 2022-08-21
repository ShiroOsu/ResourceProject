using Code.SaveSystem.Data;
using Code.ScriptableObjects;
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
        [SerializeField] private UnitData unitData;
        [SerializeField] private GameObject fovObject;
        
        private GameObject m_UnitImage;
        private readonly SoldierData m_SoldierData = new();

        private void Awake()
        {
            UnitType = UnitType.Soldier;
            TextureAssetType = TextureAssetType.Soldier;
            
            fovObject.transform.localScale = new Vector3(unitData.fieldOfView, 0f, unitData.fieldOfView);
            
            Agent = GetComponent<NavMeshAgent>();
            Agent.speed = unitData.movementSpeed;
            Agent.acceleration = unitData.acceleration;
            
            if (!m_UnitImage)
            {
                m_UnitImage = Extensions.FindObject("SoldierImage");
            }
        }

        public override void EnableFoV(bool fov = true)
        {
            fovObject.SetActive(fov);
        }

        public override GameObject GetUnitImage()
        {
            return m_UnitImage;
        }

        public override void ShouldSelect(bool select)
        {
            UIManager.Instance.UnitSelected(select, gameObject, UnitType.Soldier, m_UnitImage, unitData);
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