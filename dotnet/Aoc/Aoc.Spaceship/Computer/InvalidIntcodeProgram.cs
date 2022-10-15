using System;

namespace Aoc.Spaceship.Computer
{
    public class InvalidIntcodeProgram : Exception
    {
        public InvalidIntcodeProgram(string message) : base(message)
        {
        }
    }
}
