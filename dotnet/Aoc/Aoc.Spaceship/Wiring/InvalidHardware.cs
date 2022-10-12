using System;

namespace Aoc.Spaceship.Wiring
{
    public class InvalidHardware : Exception
    {
        public InvalidHardware(string message) : base(message) {}
    }
}
