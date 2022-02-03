using UnityEditor;
using UnityEngine;

namespace Code.Framework.FlowField
{
    public class GridDisplayDebug : MonoBehaviour
    {
        public GridController m_GridController;

        [Header("Only visible in play mode")] public bool m_EnableDebug;
        public Vector2Int m_MaxHandles;
        public Vector2Int m_StartOrigo;

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
            var style = GUI.skin.label;
            style.alignment = TextAnchor.MiddleCenter;

            for (int x = m_StartOrigo.x; x < m_MaxHandles.x; x++)
            {
                for (int y = m_StartOrigo.y; y < m_MaxHandles.y; y++)
                {
                    var center = new Vector3(cellRadius * 2f * x + cellRadius, cellRadius,
                        cellRadius * 2f * y + cellRadius);
                    var cellHeight = m_GridController.m_Terrain.SampleHeight(center);
                    var size = Vector3.one * cellRadius * 2f;

                    if (cellHeight > 0f)
                    {
                        center.y = cellHeight + cellRadius;
                    }

                    var cell = m_GridController.CurrentFlowField.Grid[x, y];

                    if (cell.Cost > 1)
                    {
                        Gizmos.color = Color.magenta;
                    }
                    else
                    {
                        Gizmos.color = Color.black;
                    }

                    Handles.Label(cell.WorldPos, cell.Cost.ToString(), style);
                    Gizmos.DrawWireCube(center, size);
                }
            }
        }
    }
}