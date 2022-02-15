using System;
using Code.Framework.Logger;
using Code.Managers;
using Code.Player;
using UnityEngine;

namespace Code.Framework.AStar
{
    public class AStarController : Singleton<AStarController>
    {
        public Terrain terrain;
        public float cellRadius;
        public LayerMask aStarMask;

        private AStarGrid m_AStarGrid;
        private Cell[,] m_Grid;
        private Vector3Int m_StartLocation;
        private MouseInputs m_MouseInputs;

        private void Awake()
        {
            m_AStarGrid = new AStarGrid(terrain, cellRadius)
            {
                mask = aStarMask
            };
            m_Grid = m_AStarGrid.GetGrid();

            m_MouseInputs = DataManager.Instance.mouseInputs;
            m_MouseInputs.CalculateAStarPath += CalculatePath;
            //var startCell = m_Grid[m_StartLocation.x, m_StartLocation.z];
        }

        private void CalculatePath()
        {
            print("CalculateAStarPath subscription succeeded");
            
            // When path is calculated, return it to mouseInputs
            m_MouseInputs.aStarPath = Vector3.zero;
        }
    }
}
