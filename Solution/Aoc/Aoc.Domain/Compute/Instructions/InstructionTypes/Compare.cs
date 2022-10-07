using System.Collections.Generic;

namespace Aoc.Domain.Compute.Instructions.InstructionTypes
{
    internal abstract class Compare : IInstruction
    {
        public int Length => 4;
        public List<ParameterMode> ParameterModes { get; }

        internal Compare(int opcode)
        {
            ParameterModes = new List<ParameterMode>();
            ParameterModes.Add(IInstruction.GetParameterMode(opcode, 2));
            ParameterModes.Add(IInstruction.GetParameterMode(opcode, 3));
        }

        internal abstract bool DoComparison(int parameter1, int parameter2);
    }
}
