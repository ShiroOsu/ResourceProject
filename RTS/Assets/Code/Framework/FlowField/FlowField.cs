using Code.Framework.Logger;
using UnityEngine;

namespace Code.Framework.FlowField
{
    public class FlowField
    {
        public Cell[,] Grid { get; private set; }
        private Vector2Int m_GridSize;
        private readonly float m_CellRadius;
        private readonly float m_CellDiameter;
        private readonly float m_MaxMountainHeight;

        public FlowField(float cellRadius, Vector2Int gridSize, float maxMountainHeight = 0.5f)
        {
            m_CellRadius = cellRadius;
            m_CellDiameter = cellRadius * 2f;
            m_GridSize = gridSize;
            m_MaxMountainHeight = maxMountainHeight;
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
            Log.Message("FlowField.cs", "FlowField Grid Created.");
            SetCellsCost();
        }

        private void SetCellsCost()
        {
            var cellHalfExtents = Vector3.one * m_CellRadius;
            var terrainMask = LayerMask.GetMask("StructureLayer");

            // 255 is max cell cost 
            foreach (var cell in Grid)
            {
                // Convert to OverlapBoxNonAlloc
                var obstacles = Physics.OverlapBox(cell.WorldPos, cellHalfExtents, Quaternion.identity, terrainMask);
                
                if (cell.WorldPos.y > m_MaxMountainHeight)
                {
                    cell.IncreaseCellCost(255);
                    continue;
                }
                
                foreach (var box in obstacles)
                {
                    if (box.gameObject.layer == LayerMask.NameToLayer("StructureLayer"))
                    {
                        cell.IncreaseCellCost(255);
                    }
                    
                    // Other terrain 
                }
            }
            Log.Message("FlowField.cs", "FlowField cell cost added.");
        }
    }
}
