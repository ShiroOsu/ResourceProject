using UnityEngine;

namespace Code.Framework.FlowField
{
    public class GridDisplayDebug : MonoBehaviour
    {
        public GridController m_GridController;
        public bool m_EnableDebug;
        
        private void OnDrawGizmos()
        {
            if (m_EnableDebug)
            {
                DrawGrid(m_GridController.GridSize, Color.black, m_GridController.m_CellRadius);
            }
        }

        private void DrawGrid(Vector2Int gridSize, Color c, float cellRadius)
        {
            Gizmos.color = c;
            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    var center = new Vector3(cellRadius * 2f * i + cellRadius, cellRadius, cellRadius * 2f * j + cellRadius);
                    var size = Vector3.one * cellRadius * 2f;

                    var cellHeight = m_GridController.m_Terrain.SampleHeight(center);
                    
                    if (cellHeight > 0f)
                    {
                        center.y = cellHeight + cellRadius;
                    }
                    
                    Gizmos.DrawWireCube(center, size);
                }
            }
        }
    }
}
