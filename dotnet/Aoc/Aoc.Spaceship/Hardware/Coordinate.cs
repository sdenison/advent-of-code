namespace Aoc.Spaceship.Hardware
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
    }
}
