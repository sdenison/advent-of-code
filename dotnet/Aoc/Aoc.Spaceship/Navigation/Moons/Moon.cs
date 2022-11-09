namespace Aoc.Spaceship.Navigation.Moons
{
    public class Moon
    {
        public Coordinate Position { get; set; }
        public Coordinate Velocity { get; set; }
        public Coordinate StepDelta { get; set; }
        public int TotalEnergy => Position.Energy * Velocity.Energy;

        public Moon(string coordinate)
        {
            Position = new Coordinate(coordinate);
            Velocity = new Coordinate(0, 0, 0);
        }
    }
}
