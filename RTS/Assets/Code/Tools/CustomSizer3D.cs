using UnityEngine;

namespace Code.Tools
{
    public class CustomSizer3D : MonoBehaviour
    {
        private Bounds m_Bounds;
        public Vector3 dimensions;
        public Transform centerOfArea;

        private void DrawArea(Color color)
        {
            Gizmos.color = color;

            if (!centerOfArea)
                return;

            SetArea();
        
            Gizmos.DrawWireCube(m_Bounds.center, m_Bounds.size);
        }

        private void SetArea()
        {
            var centerPos = centerOfArea.position;

            m_Bounds.extents = dimensions;
            m_Bounds.center = centerPos;
        }

        /// <summary>
        /// When getting the size it will be divided by GameObjects world scale
        /// So the actual size will correct even if object has been scaled.
        ///
        /// Mainly used for buildings that will have a NavMeshObstacle, that's why scale is needed.
        /// </summary>
        /// <param name="worldScale">GameObjects scale in world (LossyScale)</param>
        /// <returns></returns>
        public Vector3 GetSize(Vector3 worldScale)
        {
            SetArea();

            var avgOfWorldScale = (worldScale.x + worldScale.y + worldScale.z) / 3f;
        
            return m_Bounds.size / avgOfWorldScale;
        }

        /// <summary>
        /// No Scale
        /// </summary>
        /// <returns></returns>
        public Vector3 GetSize()
        {
            SetArea();

            return m_Bounds.size;
        }

        public void OnDrawGizmosSelected()
        {
            DrawArea(Color.magenta);
        }
    }
}
