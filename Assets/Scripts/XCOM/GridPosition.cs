namespace XCOM
{
    struct GridPosition
    {
        public int X { get; }
        public int Z { get; }

        public GridPosition(int x, int z)
        {
            X = x;
            Z = z;
        }

        public override string ToString()
        {
            return $"{X}, {Z}";
        }
    }
}