using UnityEngine;

namespace Code.Framework.FlowField
{
    public class Cell
    {
        public Vector3 WorldPos { get; }
        public Vector2Int GridIndex { get; }
        public byte Cost { get; private set; }

        public Cell(Vector3 worldPos, Vector2Int gridIndex)
        {
            WorldPos = worldPos;
            GridIndex = gridIndex;
            Cost = 1;
        }

        public void IncreaseCellCost(int amount)
        {
            if (Cost == byte.MaxValue)
                return;

            if (amount + Cost >= byte.MaxValue)
            {
                Cost = byte.MaxValue;
            }
            else
            {
                Cost += (byte)amount;
            }
        }
    }
}