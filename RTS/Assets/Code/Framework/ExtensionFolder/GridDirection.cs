using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Framework.ExtensionFolder
{
    public class GridDirection
    {
        public readonly Vector2Int Vector;

        private GridDirection(int x, int y)
        {
            Vector = new Vector2Int(x, y);
        }

        public static implicit operator Vector2Int(GridDirection direction)
        {
            return direction.Vector;
        }

        public static GridDirection GetDirectionFromV2I(Vector2Int vector)
        {
            return CardinalAndIntercardinalDirections.DefaultIfEmpty(None).FirstOrDefault(direction => direction == vector);
        }

        public static readonly GridDirection None = new(0, 0);
        public static readonly GridDirection North = new(0, 1);
        public static readonly GridDirection South = new(0, -1);
        public static readonly GridDirection East = new(1, 0);
        public static readonly GridDirection West = new(-1, 0);
        public static readonly GridDirection NorthEast = new(1, 1);
        public static readonly GridDirection NorthWest = new(-1, 1);
        public static readonly GridDirection SouthEast = new(1, -1);
        public static readonly GridDirection SouthWest = new(-1, -1);

        // Cardinal = N, S, W, E
        public static readonly List<GridDirection> CardinalDirections = new()
        {
            North,
            South,
            East,
            West
        };

        // Inter-cardinal = NW, NE, SW, SE 
        private static readonly List<GridDirection> CardinalAndIntercardinalDirections = new()
        {
            North,
            NorthEast,
            NorthWest,
            South,
            SouthEast,
            SouthWest,
            East,
            West
        };

        public static readonly List<GridDirection> AllDirections = new()
        {
            None,
            North,
            NorthEast,
            NorthWest,
            South,
            SouthEast,
            SouthWest,
            East,
            West
        };
    }
}
