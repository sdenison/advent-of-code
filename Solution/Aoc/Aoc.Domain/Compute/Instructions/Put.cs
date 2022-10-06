using System.Collections.Generic;

namespace Aoc.Domain.Compute.Instructions
{
    public class Put : IInstruction
    {
        public int Length => 2;
        public List<ParameterMode> ParameterModes { get; }

        public Put(int opcode)
        {
            ParameterModes = new List<ParameterMode>();
            ParameterModes.Add((ParameterMode) IInstruction.GetParameterMode(opcode, 2));
        }
    }
}
