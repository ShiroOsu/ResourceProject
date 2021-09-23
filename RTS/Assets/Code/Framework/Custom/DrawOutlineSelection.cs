using UnityEngine;

namespace Code.Framework.Custom
{
    public class DrawOutlineSelection : MonoBehaviour
    {
        [SerializeField] private CustomSizer m_CustomSizer;
        [SerializeField] private LineRenderer m_LineRenderer;

        private void Start()
        {
            SetLineCorners();
        }

        public void SetLineCorners()
        {
            var corners = m_CustomSizer.GetCorners();
            m_LineRenderer.loop = true;
            m_LineRenderer.numCornerVertices = 5;
            m_LineRenderer.useWorldSpace = false;

            for (int i = 0; i < corners.Length; i++)
            {
                m_LineRenderer.SetPosition(i, corners[i]);
            }
        }
    }
}