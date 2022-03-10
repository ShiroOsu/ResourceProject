using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Code.HelperClasses
{
    public class GridDirection
    {
        public Vector2Int Vector;

        private GridDirection(int x, int y)
        {
            Vector = new Vector2Int(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector2Int(GridDirection direction)
        {
            return direction.Vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GridDirection GetDirectionFromV2I(in Vector2Int vector)
        {
            return CardinalAndIntercardinalDirectionsLookup[vector];
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
        public static readonly GridDirection[] CardinalDirections = 
        {
            North,
            South,
            East,
            West
        };

        // Inter-cardinal = NW, NE, SW, SE 
        public static readonly GridDirection[] CardinalAndIntercardinalDirections =
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

        private static readonly Dictionary<Vector2Int, GridDirection> CardinalAndIntercardinalDirectionsLookup = new()
        {
            { North.Vector, North },
            { NorthEast.Vector, NorthEast },
            { NorthWest.Vector, NorthWest },
            { South.Vector, South },
            { SouthEast.Vector, SouthEast },
            { SouthWest.Vector, SouthWest },
            { East.Vector, East },
            { West.Vector, West }
        };

        public static readonly GridDirection[] AllDirections = 
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
    }
}
