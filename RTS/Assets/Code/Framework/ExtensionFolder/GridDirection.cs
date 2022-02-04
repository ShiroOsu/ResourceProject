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

        public static readonly GridDirection None = new GridDirection(0, 0);
        private static readonly GridDirection North = new GridDirection(0, 1);
        private static readonly GridDirection South = new GridDirection(0, -1);
        private static readonly GridDirection East = new GridDirection(1, 0);
        private static readonly GridDirection West = new GridDirection(-1, 0);
        private static readonly GridDirection NorthEast = new GridDirection(1, 1);
        private static readonly GridDirection NorthWest = new GridDirection(-1, 1);
        private static readonly GridDirection SouthEast = new GridDirection(1, -1);
        private static readonly GridDirection SouthWest = new GridDirection(-1, -1);

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
