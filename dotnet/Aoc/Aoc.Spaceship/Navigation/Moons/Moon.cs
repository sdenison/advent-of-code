using System.Drawing;
using System.Runtime.InteropServices.ComTypes;

namespace Aoc.Spaceship.Navigation.Moons
{
    public class Moon
    {
        public Coordinate Position { get; set; }
        public Coordinate Velocity { get; set; }
        public Coordinate StepDelta { get; set; }
        public int TotalEnergy => Position.Energy * Velocity.Energy;
        public long XStepNumber { get; set; }
        public long YStepNumber { get; set; }
        public long ZStepNumber { get; set; }

        public Moon(string coordinate)
        {
            XStepNumber = 0;
            YStepNumber = 0;
            ZStepNumber = 0;
            Position = new Coordinate(coordinate);
            Velocity = new Coordinate(0, 0, 0);
        }

        public bool Equals(Moon obj)
        {
            return Position.Equals(obj.Position) && Velocity.Equals(obj.Velocity);
        }

        public long GetHash()
        {
            return Position.X * 10000 + Position.Y * 100 * Position.Z + Velocity.Z * 1000000 * Velocity.Y * 100000000 + Velocity.X * 10000000000;
        }

        public string ToString()
        {
            return Position.ToString() + Velocity.ToString();
        }
    }
}
