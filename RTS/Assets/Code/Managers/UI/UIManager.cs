using System;
using Code.Enums;
using Code.HelperClasses;
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
        [SerializeField] private StructureInfo structureInfo;
        [SerializeField] private UnitInfo unitInfo;
        
        [Serializable] 
        public struct StructureInfo
        {
            public TMP_Text name;
            public TMP_Text armor;
            
            public void SetValues(string _name, int _armor)
            {
                name.SetText(_name);
                armor.SetText(_armor.ToString());
            }
        }
        
        [Serializable]
        public struct UnitInfo
        {
            public TMP_Text name;
            public TMP_Text attack;
            public TMP_Text attackSpeed;
            public TMP_Text armor;
            
            public void SetValues(string _name, int _attack, float _attackSpeed, int _armor)
            {
                name.SetText(_name);
                attack.SetText(_attack.ToString());
                attackSpeed.SetText(_attackSpeed.ToString());
                armor.SetText(_armor.ToString());
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

        public void SetUnitStatsInfo(UnitData data)
        {
            SwitchBetweenInfo(false);
            unitInfo.SetValues(data.unitName, data.attack, data.attackSpeed, data.armor);
        }
        
        public void SetStructureStatsInfo(StructureData data)
        {
            SwitchBetweenInfo(true);
            structureInfo.SetValues(data.structureName, data.armor);
        }

        // Switch between structure and unit info
        private void SwitchBetweenInfo(bool b)
        {
            objectWithStructureInfo.SetActive(b);
            objectWithUnitInfo.SetActive(!b);
        }
    }
}