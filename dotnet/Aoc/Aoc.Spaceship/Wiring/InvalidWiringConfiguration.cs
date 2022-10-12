using System;

namespace Aoc.Spaceship.Wiring
{
    public class InvalidWiringConfiguration : Exception
    {
        public InvalidWiringConfiguration(string message) : base(message) {}
    }
}
