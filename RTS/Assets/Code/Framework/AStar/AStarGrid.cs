using Code.Framework.Logger;
using UnityEngine;

namespace Code.Framework.AStar
{
    public enum CellState
    {
        UnChecked,
        Locked,
        Open,
    }
    
    public struct Cell
    {
        public Vector3 worldPos;
        public ushort cost;
        public CellState CellState;
    }

    public class AStarGrid
    {
        public LayerMask mask;
        private Cell[,] Grid { get; }
        private Collider[] m_Hits;

        public AStarGrid(Terrain terrain, float cellRadius)
        {
            var data = terrain.terrainData.size;
            var gridSize = new Vector2Int((int)data.x, (int)data.z);
            
            Grid = new Cell[gridSize.x, gridSize.y];

            SetCellPosition(terrain, cellRadius, gridSize);
        }

        private void SetCellPosition(Terrain terrain, float cellRadius, Vector2Int gridSize)
        {
            for (var x = 0; x < gridSize.x; x++)
            {
                for (var y = 0; y < gridSize.y; y++)
                {
                    var worldPos = new Vector3(cellRadius * 2f * x + cellRadius, 0f, cellRadius * 2f * y + cellRadius);

                    var cellHeight = terrain.SampleHeight(worldPos);
                    if (cellHeight > 0.1f)
                    {
                        worldPos.y = cellHeight + cellRadius;
                    }

                    Grid[x, y].worldPos = worldPos;
                    SetCellCost(Grid[x, y], cellRadius);
                }
            }
        }

        private void SetCellCost(Cell cell, float cellRadius)
        {
            var cellHalfExtents = Vector3.one * cellRadius;
            var overlaps = Physics.OverlapBoxNonAlloc(cell.worldPos, cellHalfExtents, m_Hits, Quaternion.identity, mask);

            if (overlaps > 0)
            {
                cell.cost = ushort.MaxValue;
            }
        }

        public Cell[,] GetGrid()
        {
            return Grid;
        }
    }
}