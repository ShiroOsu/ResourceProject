using Code.SaveSystem.Data;
using Code.ScriptableObjects;
using Code.Tools.Enums;
using Code.Tools.HelperClasses;
using Code.UI;
using UnityEngine;

namespace Code.Units.Horse
{
    public class HorseUnit : BaseUnit
    {
        [SerializeField] private GameObject selectionCircle;
        [SerializeField] private UnitData unitData;
        [SerializeField] private GameObject fovObject;
        
        private GameObject m_UnitImage;
        private readonly HorseData m_HorseData = new();

        private void Awake()
        {
            UnitType = UnitType.Horse;
            TextureAssetType = TextureAssetType.Horse;
            
            // not Z-axis ????
            fovObject.transform.localScale = new Vector3(unitData.fieldOfView, 0f, unitData.fieldOfView);
            
            Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            Agent.speed = unitData.movementSpeed;
            Agent.acceleration = unitData.acceleration;
            
            if (!m_UnitImage)
            {
                m_UnitImage = Extensions.FindObject("HorseImage");
            }
        }

        public override void EnableFoV(bool fov = true)
        {
            fovObject.SetActive(fov);
        }

        public override void ShouldSelect(bool select)
        {
            UIManager.Instance.UnitSelected(select, gameObject, UnitType.Horse, m_UnitImage, unitData);
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

        public override void Save()
        {
            m_HorseData.Save(gameObject);
            SaveData.Instance.horseData.Add(m_HorseData);
        }
    }
}