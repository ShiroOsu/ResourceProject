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
        [Header("Structures")]
        [SerializeField] private CastleUIManager castle;
        [SerializeField] private BarracksUIManager barracks;
        
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
            
            public void SetValues(string name, int armor)
            {
                this.name.SetText(name);
                this.armor.SetText(armor.ToString());
            }
        }
        
        [Serializable]
        public struct UnitInfo
        {
            public TMP_Text name;
            public TMP_Text attack;
            public TMP_Text attackSpeed;
            public TMP_Text armor;
            
            public void SetValues(string name, int attack, float attackSpeed, int armor)
            {
                this.name.SetText(name);
                this.attack.SetText(attack.ToString());
                this.attackSpeed.SetText(attackSpeed.ToString());
                this.armor.SetText(armor.ToString());
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
        // Timers
        // ------------------------------------------------------------------------

        private void SetUpTimer(GameObject timerObject, string name)
        {
            timerObject.TryGetComponent<RectTransform>(out var rectTransform);
            Extensions.FindObject(name).TryGetComponent(out RectTransform uiRectTransform);
            
            rectTransform.SetParent(uiRectTransform.transform);
            rectTransform.localScale = Vector3.one;
            rectTransform.anchoredPosition3D = uiRectTransform.anchoredPosition3D;
            rectTransform.localRotation = Quaternion.identity;
        }
        
        public void AddTimerToUI(GameObject timerObject, string nameOfUIObjectInScene)
        {
            timerObject.SetActive(false);
            SetUpTimer(timerObject, nameOfUIObjectInScene);
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