using System;
using Code.Framework;
using Code.Framework.Enums;
using Code.Framework.ExtensionFolder;
using Code.Framework.Interfaces;
using Code.Managers.Structures;
using Code.Managers.Units;
using Code.Structures;
using Code.Units;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Managers
{
    public sealed class UIManager : Singleton<UIManager>
    {
        [Header("Units")]
        [SerializeField] private BuilderUIManager builder;
        [SerializeField] private SoldierUIManager soldier;
        [SerializeField] private HorseUIManager horse;
        
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
            //public TMP_Text MovementSpeed; TODO
            
            public void SetValues(string name, int attack, float attackSpeed, int armor, float movementSpeed)
            {
                this.name.SetText(name);
                this.attack.SetText(attack.ToString());
                this.attackSpeed.SetText(attackSpeed.ToString());
                this.armor.SetText(armor.ToString());
                //MovementSpeed.SetText(movementSpeed.ToString());
            }
        }

        public void UnitSelected(bool select, GameObject unit)
        {
            unit.TryGetComponent(out IUnit iu);
            var type = iu.GetUnitType();
            
            switch (type)
            {
                case TextureAssetType.Builder:
                    builder.EnableMainUI(select, unit);
                    break;
                case TextureAssetType.Solider:
                    soldier.EnableMainUI(select, unit);
                    break;
                case TextureAssetType.Horse:
                    horse.EnableMainUI(select, unit);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void StructureSelected(StructureType type, bool select, GameObject structure)
        {
            switch (type)
            {
                case StructureType.Castle:
                    castle.EnableMainUI(select, structure);
                    break;
                case StructureType.Barracks:
                    barracks.EnableMainUI(select, structure);
                    break;
                case StructureType.Null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        // ------------------------------------------------------------------------
        // Timers
        // ------------------------------------------------------------------------

        private void SetUpTimer(GameObject timerObject, string name)
        {
            timerObject.TryGetComponent<RectTransform>(out var rectTransform);
            Extensions.FindInactiveObject(name).TryGetComponent(out RectTransform uiRectTransform);
            
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
            unitInfo.SetValues(data.unitName, data.attack, data.attackSpeed, data.armor, data.movementSpeed);
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