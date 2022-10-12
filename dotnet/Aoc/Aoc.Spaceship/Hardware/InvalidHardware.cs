using System;

namespace Aoc.Spaceship.Hardware
{
    public class InvalidHardware : Exception
    {
        public InvalidHardware(string message) : base(message) {}
    }
}
