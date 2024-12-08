namespace Day8
{
    public sealed record Position
    {
        public int X { get; init; }
        public int Y { get; init; }
        
        
        public bool Equals(Position? other)
        {
            return other is not null && X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}