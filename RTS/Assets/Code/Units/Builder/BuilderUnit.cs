using System;
using System.Collections;
using Code.Interfaces;
using Code.Managers;
using Code.Managers.Building;
using Code.SaveSystem.Data;
using Code.Tools.Debugging;
using Code.Tools.Enums;
using Code.Tools.HelperClasses;
using Code.UI;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Units.Builder
{
    public class BuilderUnit : BaseUnit
    {
        [SerializeField] private GameObject selectionCircle;
        [SerializeField] private UnitData unitData;
        
        private GameObject m_UnitImage;

        private readonly BuilderData m_BuilderData = new();
        private ShopManager m_Shop;
        private BuildManager m_BuildManager;
        
        private void Awake()
        {
            m_Shop = ShopManager.Instance;
            m_BuildManager = BuildManager.Instance;

            Agent = GetComponent<NavMeshAgent>();
            Agent.speed = unitData.movementSpeed;
            Agent.acceleration = unitData.acceleration;
            
            UnitType = UnitType.Builder;
            TextureAssetType = TextureAssetType.Builder;

            if (!m_UnitImage)
            {
                m_UnitImage = Extensions.FindObject("BuilderImage");
            }
        }

        public override void ShouldSelect(bool select)
        {
            UIManager.Instance.UnitSelected(select, gameObject, UnitType.Builder, m_UnitImage, unitData);
            ActivateSelectionCircle(select);
        }

        public override void ActivateSelectionCircle(bool active)
        {
            selectionCircle.SetActive(active);
        }
        
        public override GameObject GetUnitImage()
        {
            return m_UnitImage;
        }

        public void OnStructureBuildButton(StructureType type)
        {
            switch (type)
            {
                case StructureType.Castle:
                    if (!m_Shop.CanAffordBuilding(type))
                    {
                        Log.Print("BuilderUnit.cs", "Could not afford to create a Castle building!");
                        return;
                    }
                    m_BuildManager.InitBuild(type);
                    break;
                
                case StructureType.Barracks:
                    if (!m_Shop.CanAffordBuilding(type))
                    {
                        Log.Print("BuilderUnit.cs", "Could not afford to create a Barracks building!");
                        return;
                    }
                    m_BuildManager.InitBuild(type);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        

        public override void Save()
        {
            m_BuilderData.Save(gameObject);
            SaveData.Instance.builderData.Add(m_BuilderData);
        }
    }
}