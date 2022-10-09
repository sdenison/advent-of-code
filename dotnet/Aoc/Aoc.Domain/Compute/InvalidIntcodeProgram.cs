using System;

namespace Aoc.Domain.Compute
{
    public class InvalidIntcodeProgram : Exception
    {
        public InvalidIntcodeProgram(string message) : base(message)
        {
        }
    }
}
