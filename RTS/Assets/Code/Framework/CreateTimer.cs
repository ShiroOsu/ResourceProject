using System;
using Code.Framework.Enums;
using Code.Framework.TextureListByEnum;
using Code.Logger;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Framework
{
    // Temp
    // Each structure needs own Timer + timerUI for so they can store a buffer of creations.
    // Some structures buffer upgrades, other units etc..
    public class CreateTimer : MonoBehaviour
    {
        private static CreateTimer s_Instance;
        public static CreateTimer Instance => s_Instance ??= FindObjectOfType<CreateTimer>();

        [SerializeField] private GameObject m_Timer;
        [SerializeField] private RawImage m_CreateImage;
        [SerializeField] private Slider m_TimerFill;
        [SerializeField] private TextureList m_CreatableTextures;
        
        private bool m_TimerFinished;
        private bool m_TimerStarted;
        public bool TimerFinished { get; private set; }
        public bool TimerStarted { get; private set; }

        public void Create(UnitType u_Type = default, StructureType s_Type = default)
        {
            TimerStarted = true;
            
            if (u_Type != default)
            {
                Log.Message("CreateTimer", "type: " + u_Type);
                UnitCreate(u_Type);
            }
            else if (s_Type != default)
            {
                StructureCreate(s_Type);
            }
        }

        private void UnitCreate(UnitType type)
        {
            m_CreateImage.texture = GetUnitTexture(type);
            Log.Message("CreateTimer", "type: " + GetUnitTexture(type));
            ShowImage(true);
            
            // stuff...

            TimerFinished = true;
        }

        private void StructureCreate(StructureType type)
        {
            m_CreateImage.texture = GetStructureTexture(type);
            ShowImage(true);
        }

        public void ShowTimer(bool show)
        {
            m_Timer.SetActive(show);

            if (!m_CreateImage.texture)
            {
                ShowImage(false);
            }
        }

        private void ShowImage(bool show)
        {
            m_CreateImage.gameObject.SetActive(show);
        }

        private Texture GetUnitTexture(UnitType type)
        {
            var unitTexture = type switch
            {
                UnitType.Builder => m_CreatableTextures[TextureAssetType.Builder],
                UnitType.Solider => m_CreatableTextures[TextureAssetType.Solider],
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            return unitTexture;
        }
        
        private Texture GetStructureTexture(StructureType type)
        {
            var structureTexture = type switch
            {
                StructureType.Castle => m_CreatableTextures[TextureAssetType.Castle],
                StructureType.Barracks => m_CreatableTextures[TextureAssetType.Barracks],
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            return structureTexture;
        }
    }
}
