using System;
using System.Globalization;
using Code.Framework;
using Code.Framework.Enums;
using Code.Framework.Extensions;
using Code.Managers.Structures;
using Code.Managers.Units;
using Code.Structures;
using Code.Structures.Castle;
using Code.Units;
using NUnit.Compatibility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

namespace Code.Managers
{
    public sealed class UIManager : MonoBehaviour
    {
        private static UIManager s_Instance;
        public static UIManager Instance => s_Instance ??= FindObjectOfType<UIManager>();

        [Header("Units")]
        [SerializeField] private BuilderUIManager m_Builder;
        [SerializeField] private SoldierUIManager m_Soldier;
        [SerializeField] private HorseUIManager m_Horse;

        [Header("Structures")]
        [SerializeField] private CastleUIManager m_Castle;
        [SerializeField] private BarracksUIManager m_Barracks;

        [Header("UI")] 
        [SerializeField] private GameObject m_ObjectWithStructureInfo;
        [SerializeField] private GameObject m_ObjectWithUnitInfo;
        [SerializeField] private StructureInfo m_StructureInfo;
        [SerializeField] private UnitInfo m_UnitInfo;
        
        [Serializable] 
        public struct StructureInfo
        {
            public TMP_Text Name;
            public TMP_Text Armor;
            
            public void SetValues(string name, int armor)
            {
                Name.SetText(name);
                Armor.SetText(armor.ToString());
            }
        }
        
        [Serializable]
        public struct UnitInfo
        {
            public TMP_Text Name;
            public TMP_Text Attack;
            public TMP_Text AttackSpeed;
            public TMP_Text Armor;
            
            public void SetValues(string name, int attack, float attackSpeed, int armor)
            {
                Name.SetText(name);
                Attack.SetText(attack.ToString());
                AttackSpeed.SetText(attackSpeed.ToString());
                Armor.SetText(armor.ToString());
            }
        }

        public void UnitSelected(UnitType type, bool select, GameObject unit)
        {
            switch (type)
            {
                case UnitType.Builder:
                    m_Builder.EnableMainUI(select, unit);
                    break;
                case UnitType.Solider:
                    m_Soldier.EnableMainUI(select, unit);
                    break;
                case UnitType.Horse:
                    m_Horse.EnableMainUI(select, unit);
                    break;
                case UnitType.Null:
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
                    m_Castle.EnableMainUI(select, structure);
                    break;
                case StructureType.Barracks:
                    m_Barracks.EnableMainUI(select, structure);
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
            Extensions.FindInactiveObject(name).TryGetComponent(out RectTransform UIRectTransform);
            
            rectTransform.SetParent(UIRectTransform.transform);
            rectTransform.localScale = Vector3.one;
            rectTransform.anchoredPosition3D = UIRectTransform.anchoredPosition3D;
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
            
            m_UnitInfo.SetValues(data.unitName, data.attack, data.attackSpeed, data.armor);
        }
        
        public void SetStructureStatsInfo(StructureData data)
        {
            SwitchBetweenInfo(true);
            
            m_StructureInfo.SetValues(data.structureName, data.armor);
        }

        private void SwitchBetweenInfo(bool b)
        {
            m_ObjectWithStructureInfo.SetActive(b);
            m_ObjectWithUnitInfo.SetActive(!b);
        }
    }
}