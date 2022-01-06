using System;
using Code.Framework.ExtensionFolder;
using Code.Framework.Logger;
using UnityEngine;

namespace Code.Framework.FlowField
{
    public class GridController : MonoBehaviour
    {
        public float m_CellRadius = 0.5f;
        private FlowField CurrentFlowField;
        public Terrain m_Terrain;
        public Vector2Int GridSize { get; private set; }

        private void InitializeFlowField()
        {
            var terrainData = m_Terrain.terrainData;
            GridSize = new Vector2Int((int)terrainData.bounds.max.x, (int)terrainData.bounds.max.z);
            
            CurrentFlowField = new FlowField(m_CellRadius, GridSize);
            CurrentFlowField.CreateGrid(m_Terrain);
        }

        private void Update()
        {
            if (!Extensions.WasMousePressedThisFrame())
                return;

            if (CurrentFlowField is null)
                return;
            
            InitializeFlowField();
            Log.Message("GridController", "Initialized FlowField");
        }
    }
}
