using System;
using UnityEngine;

namespace XCOM.Grid
{
    public struct GridPosition
    {
        public int x { get; }
        public int z { get; }

        public GridPosition(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, z);
        }

        public override string ToString()
        {
            return $"x: {x}; z: {z}";
        }

        public override bool Equals(object obj)
        {
            return obj is GridPosition position &&
                x == position.x &&
                z == position.z;
        }

        public bool Equals(GridPosition other)
        {
            return this == other;
        }

        public static float Distance(GridPosition a, GridPosition b)
        {
            float dx = b.x - a.x;
            float dz = b.z - a.z;
            
            return Mathf.Sqrt(Mathf.Pow(dx, 2) + Mathf.Pow(dz, 2));
        }

        public static bool operator ==(GridPosition a, GridPosition b)
        {
            return a.x == b.x && a.z == b.z;
        }

        public static bool operator !=(GridPosition a, GridPosition b)
        {
            return !(a == b);
        }

        public static GridPosition operator +(GridPosition a, GridPosition b)
        {
            return new GridPosition(a.x + b.x, a.z + b.z);
        }

        public static GridPosition operator -(GridPosition a, GridPosition b)
        {
            return new GridPosition(a.x - b.x, a.z - b.z);
        }
    }
}