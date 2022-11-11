using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
using System;

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

        //public bool Equals(object obj)
        //{
        //    return Position.Equals(obj.Position) && Velocity.Equals(obj.Velocity);
        //}

        public long GetHash()
        {
            return Math.Abs(Position.X) * 10000000000 + Math.Abs(Position.Y) * 100000000 + Math.Abs(Position.Z) * 1000000 + Math.Abs(Velocity.X) * 10000 + Math.Abs(Velocity.Y) * 100 + Math.Abs(Velocity.Z);
        }

        public string ToString()
        {
            return GetHash().ToString();
        }
    }
}
