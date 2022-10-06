using System.Collections.Generic;

namespace Aoc.Domain.Compute.Instructions
{
    public class Halt : IInstruction
    {
        public int Length => 1;
        public List<ParameterMode> ParameterModes => throw new System.NotImplementedException();
    }
}
