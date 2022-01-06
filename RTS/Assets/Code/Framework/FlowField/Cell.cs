using UnityEngine;

namespace Code.Framework.FlowField
{
    public class Cell
    {
        public Vector3 WorldPos { get; }
        public Vector2Int GridIndex { get;}

        public Cell(Vector3 worldPos, Vector2Int gridIndex)
        {
            WorldPos = worldPos;
            GridIndex = gridIndex;
        }
    }
}