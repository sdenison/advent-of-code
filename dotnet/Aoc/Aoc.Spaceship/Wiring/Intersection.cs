namespace Aoc.Spaceship.Hardware
{
    public class Intersection
    {
        public Coordinate Coordinate { get; set; }
        public int Steps { get; set; }

        public Intersection(Coordinate coordinate, int steps)
        {
            Coordinate = coordinate;
            Steps = steps;
        }
    }
}
