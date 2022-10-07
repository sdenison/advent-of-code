using System.Collections.Generic;

namespace Aoc.Domain.Compute.Instructions
{
    public class Put : Instruction
    {
        public override int Length => 2;
        public List<ParameterMode> ParameterModes { get; }

        public Put(int opcode)
        {
            ParameterModes = new List<ParameterMode>();
            ParameterModes.Add(GetParameterMode(opcode, 2));
        }
    }
}
