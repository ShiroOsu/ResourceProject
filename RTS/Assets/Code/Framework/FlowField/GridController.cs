using Code.Managers;
using UnityEngine;

namespace Code.Framework.FlowField
{
    public class GridController : MonoBehaviour
    {
        public float m_CellRadius = 0.5f;

        [Tooltip("Max Height of Mountain that can be walked on")]
        public float m_MaxMountainHeight = 0.8f;

        private DataManager m_Data;

        public FlowField CurrentFlowField { get; private set; }
        public Terrain m_Terrain;
        public Vector2Int GridSize { get; private set; }

        private void Awake()
        {
            m_Data = DataManager.Instance;
        }

        public void InitializeFlowField()
        {
            var terrainData = m_Terrain.terrainData;
            GridSize = new Vector2Int((int)terrainData.bounds.max.x, (int)terrainData.bounds.max.z);

            CurrentFlowField = new FlowField(m_CellRadius, GridSize, m_MaxMountainHeight);
            CurrentFlowField.CreateGrid(m_Terrain);
        }
    }
}