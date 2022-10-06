using System.Collections.Generic;

namespace Aoc.Domain.Compute.Instructions
{
    public class Display : IInstruction
    {
        public int Length => 2;
        public List<ParameterMode> ParameterModes { get; }
    }
}
