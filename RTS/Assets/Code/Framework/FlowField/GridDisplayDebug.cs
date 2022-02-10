using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Framework.FlowField
{
    public class GridDisplayDebug : MonoBehaviour
    {
        [FormerlySerializedAs("m_GridController")] public GridController gridController;

        [FormerlySerializedAs("m_EnableDebug")] [Header("Only visible in play mode")] public bool enableDebug;
        [FormerlySerializedAs("m_MaxHandles")] public Vector2Int maxHandles;
        [FormerlySerializedAs("m_StartOrigo")] public Vector2Int startOrigo;
        [FormerlySerializedAs("m_SpriteArrow")] public Sprite spriteArrow;

        private void OnDrawGizmos()
        {
            if (enableDebug)
            {
                DrawGrid(gridController.GridSize, Color.black, gridController.cellRadius);
            }
        }

        private void DrawGrid(Vector2Int gridSize, Color c, float cellRadius)
        {
            Gizmos.color = c;
            var style = GUI.skin.label;
            style.alignment = TextAnchor.MiddleCenter;

            for (int x = startOrigo.x; x < maxHandles.x; x++)
            {
                for (int y = startOrigo.y; y < maxHandles.y; y++)
                {
                    var center = new Vector3(cellRadius * 2f * x + cellRadius, cellRadius,
                        cellRadius * 2f * y + cellRadius);
                    var cellHeight = gridController.terrain.SampleHeight(center);
                    var size = Vector3.one * cellRadius * 2f;

                    if (cellHeight > 0f)
                    {
                        center.y = cellHeight + cellRadius;
                    }

                    var cell = gridController.CurrentFlowField.Grid[x, y];

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