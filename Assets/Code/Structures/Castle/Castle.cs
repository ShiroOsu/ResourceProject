using Code.Framework.Enums;
using Code.Framework.Interfaces;
using Code.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Structures.Castle
{
    public class Castle : MonoBehaviour, IStructure
    {
        // Spawn location for builders
        private Vector3 m_UnitSpawnPoint;
        private GameObject m_Flag = null;

        private void Update()
        {
            if (Mouse.current.rightButton.isPressed)
            {
                SetFlagPosition();
            }
        }

        private void SetFlagPosition()
        {
            m_Flag = FlagManager.Instance.SetSpawnFlag();
            m_UnitSpawnPoint = m_Flag.transform.position;
        }

        public void OnSpawnBuilderButton()
        {
            SpawnManager.Instance.SpawnUnit(UnitType.Builder, gameObject.transform.position, m_UnitSpawnPoint);
        }

        public void Destroy()
        {
            Destroy();
        }

        public void Upgrade()
        {
            Debug.Log(transform.name + " upgrade");
        }

        public void ShouldSelect(bool select)
        {
            UIManager.Instance.StructureSelected(StructureType.Castle, select, gameObject);

            if (m_Flag != null)
                m_Flag.SetActive(select);
        }
    }
}
