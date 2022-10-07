using System.Collections.Generic;

namespace Aoc.Domain.Compute.Instructions.InstructionTypes
{
    public abstract class Math : IInstruction
    {
        public int Length => 4;
        public List<ParameterMode> ParameterModes { get; }

        public abstract int ExecuteOperation(int parameter1, int parameter2);

        protected Math(int opcode)
        {
            ParameterModes = new List<ParameterMode>();
            ParameterModes.Add(IInstruction.GetParameterMode(opcode, 2));
            ParameterModes.Add(IInstruction.GetParameterMode(opcode, 3));
        }
    }
}
