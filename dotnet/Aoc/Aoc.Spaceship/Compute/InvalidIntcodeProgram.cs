using System;

namespace Aoc.Spaceship.Compute
{
    public class InvalidIntcodeProgram : Exception
    {
        public InvalidIntcodeProgram(string message) : base(message)
        {
        }
    }
}
