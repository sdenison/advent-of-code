namespace Aoc.Spaceship.Hardware
{
    public class Vector
    {
        public Coordinate Position { get; set; }
        public Axis Axis { get; set; }

        public Vector(Coordinate position, Axis axis)
        {
            Position = position;
            Axis = axis;
        }
    }
}
