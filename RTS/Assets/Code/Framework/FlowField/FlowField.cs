using System.Collections.Generic;
using Code.Framework.ExtensionFolder;
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
        public Cell m_DestinationCell;
        private readonly Collider[] hits = new Collider[1];

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
                    var worldPos = new Vector3(m_CellDiameter * x + m_CellRadius, m_CellRadius,
                        m_CellDiameter * y + m_CellRadius);

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

            // Temp, set in inspector
            var terrainMask = LayerMask.GetMask("StructureLayer");

            // 255 is max cell cost 
            foreach (var cell in Grid)
            {
                Physics.OverlapBoxNonAlloc(cell.WorldPos, cellHalfExtents, hits, Quaternion.identity, terrainMask);

                if (cell.WorldPos.y > m_MaxMountainHeight)
                {
                    cell.IncreaseCellCost(255);
                    continue;
                }

                if (hits[0] is not null)
                {
                    if (hits[0].gameObject.layer == LayerMask.NameToLayer("StructureLayer"))
                    {
                        cell.IncreaseCellCost(255);
                    }
                    
                    // Other terrain
                }

                // Clear hits
                hits[0] = null;
            }

            Log.Message("FlowField.cs", "FlowField cell cost added.");
        }

        public void CreateIntegrationField(Cell destinationCell)
        {
            m_DestinationCell = destinationCell;
            m_DestinationCell.Cost = 0;
            m_DestinationCell.BestCost = 0;

            Queue<Cell> cellsToCheck = new();
            cellsToCheck.Enqueue(m_DestinationCell);

            while (cellsToCheck.Count > 0)
            {
                var currentCell = cellsToCheck.Dequeue();
                var currentNeighbors = GetNeighborCells(currentCell.GridIndex, GridDirection.CardinalDirections);

                foreach (var neighbor in currentNeighbors)
                {
                    if (neighbor.Cost == byte.MaxValue)
                    {
                        continue;
                    }

                    if (neighbor.Cost + currentCell.BestCost < neighbor.BestCost)
                    {
                        neighbor.BestCost = (ushort)(neighbor.Cost + currentCell.BestCost);
                        cellsToCheck.Enqueue(neighbor);
                    }
                }
            }

            Log.Message("FlowField.cs", "Integration field created.");
        }

        public void CreateFlowField()
        {
            foreach (var currentCell in Grid)
            {
                var currentNeighbors = GetNeighborCells(currentCell.GridIndex, GridDirection.AllDirections);
                int bestCost = currentCell.BestCost;

                foreach (var currentNeighbor in currentNeighbors)
                {
                    if (currentNeighbor.BestCost < bestCost)
                    {
                        bestCost = currentNeighbor.BestCost;
                        currentCell.BestDirection =
                            GridDirection.GetDirectionFromV2I(currentNeighbor.GridIndex - currentCell.GridIndex);
                    }
                }
            }

            Log.Message("FlowField.cs", "Flow Field created.");
        }

        private IEnumerable<Cell> GetNeighborCells(Vector2Int gridIndex, List<GridDirection> directions)
        {
            var neighborCells = new List<Cell>();

            foreach (var currentDirection in directions)
            {
                var newNeighbor = GetCellAtRelativePos(gridIndex, currentDirection);
                if (newNeighbor is not null)
                {
                    neighborCells.Add(newNeighbor);
                }
            }

            return neighborCells;
        }

        private Cell GetCellAtRelativePos(Vector2Int orignPos, Vector2Int relativePos)
        {
            var finalPos = orignPos + relativePos;

            if (finalPos.x < 0 || finalPos.x >= m_GridSize.x || finalPos.y < 0 || finalPos.y >= m_GridSize.y)
            {
                return null;
            }
            else
            {
                return Grid[finalPos.x, finalPos.y];
            }
        }

        public Cell GetCellFromWorldPos(Vector3 worldPos)
        {
            float percentX = worldPos.x / (m_GridSize.x * m_CellDiameter);
            float percentY = worldPos.z / (m_GridSize.y * m_CellDiameter);
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            var x = (int)Mathf.Clamp(Mathf.FloorToInt((m_GridSize.x) * percentX), 0f, m_GridSize.x - 1f);
            var y = (int)Mathf.Clamp(Mathf.FloorToInt((m_GridSize.y) * percentY), 0f, m_GridSize.y - 1f);
            return Grid[x, y];
        }
    }
}