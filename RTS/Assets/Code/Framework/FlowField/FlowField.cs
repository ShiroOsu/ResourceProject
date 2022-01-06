using UnityEngine;

namespace Code.Framework.FlowField
{
    public class FlowField
    {
        public Cell[,] Grid { get; private set; }
        private Vector2Int m_GridSize;
        private readonly float m_CellRadius;

        private readonly float m_CellDiameter;

        public FlowField(float cellRadius, Vector2Int gridSize)
        {
            m_CellRadius = cellRadius;
            m_CellDiameter = cellRadius * 2f;
            m_GridSize = gridSize;
        }

        public void CreateGrid(Terrain terrain)
        {
            Grid = new Cell[m_GridSize.x, m_GridSize.y];

            for (int x = 0; x < m_GridSize.x; x++)
            {
                for (int y = 0; y < m_GridSize.y; y++)
                {
                    var worldPos = new Vector3(m_CellDiameter * x + m_CellRadius, m_CellRadius, m_CellDiameter * y + m_CellRadius);

                    var cellHeight = terrain.SampleHeight(worldPos);
                    if (cellHeight > 0f)
                    {
                        worldPos.y = cellHeight + m_CellRadius;
                    }
                    
                    Grid[x, y] = new Cell(worldPos, new Vector2Int(x, y));
                }
            }
        }
    }
}
