using System;
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

        public void SetRotation(float angle)
        {
            m_CustomSizer.angle = angle;
        }

        public void SetLineCorners()
        {
            var corners = m_CustomSizer.GetCorners();
            m_LineRenderer.loop = true;
            m_LineRenderer.numCornerVertices = 5;

            for (int i = 0; i < corners.Length; i++)
            {
                corners[i].y += 0.5f;

                m_LineRenderer.SetPosition(i, corners[i]);
            }
        }
    }
}