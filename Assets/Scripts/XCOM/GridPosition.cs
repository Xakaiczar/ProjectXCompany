namespace XCOM
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

        public override string ToString()
        {
            return $"x: {x}; z: {z}";
        }
    }
}