namespace Aoc.Spaceship.Hardware
{
    public class Vector
    {
        public Coordinate Position { get; set; }
        public Direction Direction { get; set; }

        public Vector(Coordinate position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }
    }
}
