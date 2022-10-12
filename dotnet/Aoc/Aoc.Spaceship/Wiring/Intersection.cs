namespace Aoc.Spaceship.Wiring
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
