using System;

namespace Aoc.Spaceship.Navigation.Moons
{
    public class AxisSnapshot
    {
        public int Position0 { get; set; }
        public int Position1 { get; set; }
        public int Position2 { get; set; }
        public int Position3 { get; set; }
        public int Velocity0 { get; set; }
        public int Velocity1 { get; set; }
        public int Velocity2 { get; set; }
        public int Velocity3 { get; set; }
        public long StepNumber { get; set; }

        public long GetLong()
        {
            return Math.Abs(Position0) * 1000000000000 + Math.Abs(Position1) * 10000000000 + Math.Abs(Position2) * 100000000 + Math.Abs(Position3) * 1000000 +
                   Math.Abs(Velocity0) * 10000 + Math.Abs(Velocity1) * 100 + Math.Abs(Velocity2) + Math.Abs(Velocity3);
        }
    }
}
