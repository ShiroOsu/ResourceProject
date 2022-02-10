using Code.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Framework.FlowField
{
    public class GridController : MonoBehaviour
    {
        [FormerlySerializedAs("m_CellRadius")] public float cellRadius = 0.5f;

        [FormerlySerializedAs("m_MaxMountainHeight")] [Tooltip("Max Height of Mountain that can be walked on")]
        public float maxMountainHeight = 0.8f;

        private DataManager m_Data;

        public FlowField CurrentFlowField { get; private set; }
        [FormerlySerializedAs("m_Terrain")] public Terrain terrain;
        public Vector2Int GridSize { get; private set; }

        private void Awake()
        {
            m_Data = DataManager.Instance;
        }

        public void InitializeFlowField()
        {
            var terrainData = terrain.terrainData;
            GridSize = new Vector2Int((int)terrainData.bounds.max.x, (int)terrainData.bounds.max.z);

            CurrentFlowField = new FlowField(cellRadius, GridSize, maxMountainHeight);
            CurrentFlowField.CreateGrid(terrain);
        }
    }
}