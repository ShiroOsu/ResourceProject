using Code.Framework.ExtensionFolder;
using UnityEngine;

namespace Code.Framework.FlowField
{
    public class Cell
    {
        public Vector3 WorldPos { get; }
        public Vector2Int GridIndex { get; }
        public byte Cost { get; set; }
        public ushort BestCost { get; set; }
        public GridDirection BestDirection { get; set; }
        

        public Cell(Vector3 worldPos, Vector2Int gridIndex)
        {
            WorldPos = worldPos;
            GridIndex = gridIndex;
            Cost = 1;
            BestCost = ushort.MaxValue;
            BestDirection = GridDirection.None;
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