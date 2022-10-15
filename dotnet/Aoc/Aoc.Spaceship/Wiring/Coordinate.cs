namespace Aoc.Spaceship.Wiring
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Coordinate obj)
        {
            return X == obj.X && Y == obj.Y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Coordinate);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}
