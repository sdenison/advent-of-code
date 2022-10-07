using System.Collections.Generic;

namespace Aoc.Domain.Compute.Instructions.InstructionTypes
{
    internal abstract class Jump : IInstruction
    {
        public int Length => 3;
        public List<ParameterMode> ParameterModes { get; }

        internal Jump(int opcode)
        {
            ParameterModes = new List<ParameterMode>();
            ParameterModes.Add(IInstruction.GetParameterMode(opcode, 2));
            ParameterModes.Add(IInstruction.GetParameterMode(opcode, 3));
        }

        public abstract bool ShouldJump(int parameter);
    }
}
