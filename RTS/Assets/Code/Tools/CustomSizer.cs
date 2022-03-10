using UnityEngine;

namespace Code.Tools
{
    public class CustomSizer : MonoBehaviour
    {
        private Rect m_SizeArea;
        public Vector2 widthAndLength;
        public Transform centerOfArea;
        private Vector3[] m_Corners;

        private void DrawArea(Color color, bool draw)
        {
            if (!centerOfArea)
                return;

            var centerPoint = centerOfArea.localPosition;
            
            // Show Area in Scene view
            ShowAreaInScene();

            var a = new Vector3(m_SizeArea.xMin, centerPoint.y, m_SizeArea.yMin);
            var b = new Vector3(m_SizeArea.xMax, centerPoint.y, m_SizeArea.yMin);
            var c = new Vector3(m_SizeArea.xMax, centerPoint.y, m_SizeArea.yMax);
            var d = new Vector3(m_SizeArea.xMin, centerPoint.y, m_SizeArea.yMax);
            
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
            m_SizeArea.size = new Vector2(widthAndLength.x, widthAndLength.y);
            m_SizeArea.center = Vector2.zero;
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