using System;
using UnityEngine;

namespace Code.Framework.Custom
{
    public class CustomSizer : MonoBehaviour
    {
        private Rect m_SizeArea;
        public Vector2 m_WidthAndHeight; // width & length
        public Transform m_CenterOfArea;
        public float angle;
        private Vector3[] m_Corners;

        private void DrawArea(Color color, bool draw)
        {
            if (!m_CenterOfArea)
                return;

            var centerPoint = m_CenterOfArea.position;
            var quaternion = Quaternion.AngleAxis(angle, Vector3.up);
            
            // Show Area in Scene view
            ShowAreaInScene();

            var a = quaternion * new Vector3(m_SizeArea.xMin, centerPoint.y, m_SizeArea.yMin);
            var b = quaternion * new Vector3(m_SizeArea.xMax, centerPoint.y, m_SizeArea.yMin);
            var c = quaternion * new Vector3(m_SizeArea.xMax, centerPoint.y, m_SizeArea.yMax);
            var d = quaternion * new Vector3(m_SizeArea.xMin, centerPoint.y, m_SizeArea.yMax);
            
            SetCorners(new[] { a,b,c,d });

            if (!draw)
                return;
            
            Gizmos.color = color;
            Gizmos.DrawLine(a, b);
            Gizmos.DrawLine(b, c);
            Gizmos.DrawLine(c, d);
            Gizmos.DrawLine(d, a);
        }

        private void ShowAreaInScene()
        {
            var centerPos = m_CenterOfArea.position;

            m_SizeArea.width = m_WidthAndHeight.x;
            m_SizeArea.height = m_WidthAndHeight.y;
            m_SizeArea.center = new Vector2(centerPos.x, centerPos.z);
        }

        private void SetCorners(Vector3[] corners)
        {
            m_Corners = corners;
        }

        public Vector3[] GetCorners()
        {
            DrawArea(Color.magenta, false);
            return m_Corners;
        }

        public Rect GetSizeArea()
        {
            return m_SizeArea;
        }

        public void OnDrawGizmosSelected()
        {
            DrawArea(Color.magenta, true);
        }
    }
}