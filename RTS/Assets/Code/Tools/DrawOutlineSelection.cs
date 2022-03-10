using UnityEngine;

namespace Code.Tools
{
    public class DrawOutlineSelection : MonoBehaviour
    {
        [SerializeField] private CustomSizer customSizer;
        [SerializeField] private LineRenderer lineRenderer;

        private void Start()
        {
            SetLineCorners();
        }

        private void SetLineCorners()
        {
            var corners = customSizer.GetCorners();
            lineRenderer.loop = true;
            lineRenderer.numCornerVertices = 5;
            lineRenderer.useWorldSpace = false;

            for (var i = 0; i < corners.Length; i++)
            {
                lineRenderer.SetPosition(i, corners[i]);
            }
        }
    }
}