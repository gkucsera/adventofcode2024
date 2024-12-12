namespace Day12
{
    public class Region
    {
        private HashSet<Position> Positions { get; }
        public int Size => Positions.Count;

        public int Perimeter { get; }
        public int Sides { get; }

        public Region(HashSet<Position> positions)
        {
            Positions = positions;
            Perimeter = CalculatePerimeter();
            Sides = CalculateSides(Positions);
        }

        private int CalculatePerimeter()
        {
            var result = 0;
            foreach (var position in Positions)
            {
                if (!Positions.Contains(new Position(position.X + 1, position.Y)))
                    result++;
                if (!Positions.Contains(new Position(position.X, position.Y + 1)))
                    result++;
                if (!Positions.Contains(new Position(position.X - 1, position.Y)))
                    result++;
                if (!Positions.Contains(new Position(position.X, position.Y - 1)))
                    result++;
            }

            return result;
        }

        private int CalculateSides(HashSet<Position> positions)
        {
            if (positions.Count == 1)
                return 4;

            var result = 0;
            foreach (var position in positions)
            {
                var adjacents = GetAdjacents(position, positions).ToArray();
                result += adjacents.Length switch
                {
                    1 => 2,
                    2 => GetTwoAdjacent(adjacents, position, positions),
                    3 => GetThreeAdjacent(adjacents, position, positions),
                    4 => GetFourAdjacent(position, positions),
                    _ => throw new Exception("adjacent error")
                };
            }

            return result;
        }

        private static int GetTwoAdjacent(Position[] adjacents, Position position, HashSet<Position> positions)
        {
            // row
            if ((adjacents[0].Y == adjacents[1].Y && Math.Abs(adjacents[0].X - adjacents[1].X) == 2) ||
                (adjacents[0].X == adjacents[1].X && Math.Abs(adjacents[0].Y - adjacents[1].Y) == 2))
                return 0;

            // angle
            var diffY = adjacents.Single(item => item.X == position.X).Y - position.Y;
            var diffX = adjacents.Single(item => item.Y == position.Y).X - position.X;

            if (positions.Contains(new Position(position.X + diffX, position.Y + diffY)))
            {
                return 1;
            }

            return 2;
        }

        private int GetFourAdjacent(Position position, HashSet<Position> positions)
        {
            var result = 0;
            if (!positions.Contains(new Position(position.X - 1, position.Y - 1)))
                result++;
            if (!positions.Contains(new Position(position.X - 1, position.Y + 1)))
                result++;
            if (!positions.Contains(new Position(position.X + 1, position.Y - 1)))
                result++;
            if (!positions.Contains(new Position(position.X + 1, position.Y + 1)))
                result++;

            return result;
        }
        private int GetThreeAdjacent(Position[] adjacents, Position position, HashSet<Position> positions)
        {
            var yMatch = adjacents.Count(item => item.Y == position.Y) == 2;
            var xMatch = adjacents.Count(item => item.X == position.X) == 2;
            if (yMatch && xMatch)
                throw new Exception("three match error");

            var result = 2;

            if (yMatch)
            {
                var diff = adjacents.Single(item => item.Y != position.Y);
                var diffY = diff.Y - position.Y;
                if (positions.Contains(new Position(position.X - 1, position.Y + diffY)))
                {
                    result--;
                }

                if (positions.Contains(new Position(position.X + 1, position.Y + diffY)))
                {
                    result--;
                }

                return result;
            }
            else
            {
                var diff = adjacents.Single(item => item.X != position.X);
                var diffX = diff.X - position.X;
                if (positions.Contains(new Position(position.X + diffX, position.Y + 1)))
                {
                    result--;
                }

                if (positions.Contains(new Position(position.X + diffX, position.Y - 1)))
                {
                    result--;
                }
                return result;
            }
        }


        private IEnumerable<Position> GetAdjacents(Position position, HashSet<Position> positions)
        {
            var top = new Position(position.X, position.Y - 1);
            if (positions.Contains(top))
                yield return top;
            var bottom = new Position(position.X, position.Y + 1);
            if (positions.Contains(bottom))
                yield return bottom;
            var left = new Position(position.X - 1, position.Y);
            if (positions.Contains(left))
                yield return left;
            var right = new Position(position.X + 1, position.Y);
            if (positions.Contains(right))
                yield return right;
        }
    }
}