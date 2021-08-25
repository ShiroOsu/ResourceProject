using UnityEngine;

namespace Code.Managers.Building
{
    public class BuildComponents : MonoBehaviour
    {
        public GameObject m_BuildComponents;
        public CustomSizer3D m_BuildingBounds;
        public BoxCollider m_BoxCollider;
        [HideInInspector] public bool m_InTrigger;

        private void Awake()
        {
            SetupColliderBounds();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out TerrainCollider _))
            {
                m_InTrigger = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent(out TerrainCollider _))
            {
                m_InTrigger = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out TerrainCollider _))
            {
                m_InTrigger = false;
            }
        }

        private void SetupColliderBounds()
        {
            m_BoxCollider.center = m_BuildingBounds.m_CenterOfArea.position;
            m_BoxCollider.size = m_BuildingBounds.GetSize();
        }
    }
}
