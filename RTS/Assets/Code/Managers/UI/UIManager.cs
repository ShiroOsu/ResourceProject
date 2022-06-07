using System;
using Code.Debugging;
using Code.Enums;
using Code.HelperClasses;
using Code.Managers.Resource;
using Code.Managers.Structures;
using Code.Managers.Units;
using Code.ScriptableObjects;
using Code.Units;
using TMPro;
using UnityEngine;

namespace Code.Managers.UI
{
    public sealed class UIManager : Singleton<UIManager>
    {
        [Header("UI")] 
        [SerializeField] private GameObject objectWithStructureInfo;
        [SerializeField] private GameObject objectWithUnitInfo;
        [SerializeField] private GameObject objectWithResourceInfo;
        [SerializeField] private StructureInfo structureInfo;
        [SerializeField] private UnitInfo unitInfo;
        [SerializeField] private ResourceInfo resourceInfo;
        
        [Serializable] 
        public struct StructureInfo
        {
            public TMP_Text structureName;
            public TMP_Text structureArmor;
            
            public void SetValues(string name, int armor)
            {
                structureName.SetText(name);
                structureArmor.SetText(armor.ToString());
            }
        }
        
        [Serializable]
        public struct UnitInfo
        {
            public TMP_Text unitName;
            public TMP_Text unitAttack;
            public TMP_Text unitAttackSpeed;
            public TMP_Text unitArmor;
            
            public void SetValues(string name, int attack, float attackSpeed, int armor)
            {
                unitName.SetText(name);
                unitAttack.SetText(attack.ToString());
                unitAttackSpeed.SetText(attackSpeed.ToString());
                unitArmor.SetText(armor.ToString());
            }
        }
        
        [Serializable]
        public struct ResourceInfo
        {
            public TMP_Text resourceName;
            public TMP_Text resourceArmor;
            public TMP_Text resources;
            
            public void SetValues(string name, uint resourcesLeft, int armor)
            {
                resourceName.SetText(name);
                resources.SetText(resourcesLeft.ToString());
                resourceArmor.SetText(armor.ToString());
            }

            public void UpdateResourcesLeft(uint amount)
            {
                resources.SetText(amount.ToString());
            }
        }

        public void UnitSelected(bool select, GameObject unit, UnitType type, GameObject image, UnitData data)
        {
            UnitUIProcessor.EnableUIForUnit(select, unit, type, image, data);
        }

        public void StructureSelected(bool select, GameObject structure, StructureType type, GameObject image, StructureData data)
        {
            StructureUIProcessor.EnableUIForStructure(select, structure, type, image, data);
        }

        public void ResourceSelected(bool select, GameObject resource, ResourceType type, GameObject image, ResourceData data)
        {
            ResourceUIProcessor.EnableUIForResource(select, resource, type, image, data);
        }
        
        // ------------------------------------------------------------------------
        // Timer
        // ------------------------------------------------------------------------
        
        public void AddTimerToUI(GameObject timerObject, GameObject structureUIMiddle)
        {
            timerObject.SetActive(false);
            timerObject.TryGetComponent<RectTransform>(out var timerUIRect);
            structureUIMiddle.TryGetComponent(out RectTransform structureUIRect);
            
            timerUIRect.SetParent(structureUIRect.transform);
            timerUIRect.localScale = Vector3.one;
            timerUIRect.anchoredPosition3D = structureUIRect.anchoredPosition3D;
            timerUIRect.localRotation = Quaternion.identity;
        }
        
        // ------------------------------------------------------------------------
        // Info (Stats etc ... )
        // ------------------------------------------------------------------------
        
        public void SetUnitStatsInfo(UnitData data, bool active)
        {
            SetDataInfoActive(DataType.Unit, active);
            unitInfo.SetValues(data.unitName, data.attack, data.attackSpeed, data.armor);
        }
        
        public void SetStructureStatsInfo(StructureData data, bool active)
        {
            SetDataInfoActive(DataType.Structure, active);
            structureInfo.SetValues(data.structureName, data.armor);
        }
        
        public void SetResourceStatsInfo(ResourceData data, bool active)
        {
            SetDataInfoActive(DataType.Resource, active);
            resourceInfo.SetValues(data.resourceName, data.resourcesLeft, data.armor);
        }
        
        private void SetDataInfoActive(DataType type, bool active)
        {
            switch (type)
            {
                case DataType.Unit:
                    objectWithUnitInfo.SetActive(active);
                    break;
                case DataType.Structure:
                    objectWithStructureInfo.SetActive(active);
                    break;
                case DataType.Resource:
                    objectWithResourceInfo.SetActive(active);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}