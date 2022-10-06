using System.Collections.Generic;

namespace Aoc.Domain.Compute.Instructions.InstructionTypes
{
    public abstract class MathInstruction : IInstruction
    {
        public int Length => 4;
        public List<ParameterMode> ParameterModes { get; }

        public abstract int ExecuteOperation(int parameter1, int parameter2);

        protected MathInstruction(int opcode)
        {
            ParameterModes = new List<ParameterMode>();
            ParameterModes.Add(IInstruction.GetParameterMode(opcode, 2));
            ParameterModes.Add(IInstruction.GetParameterMode(opcode, 3));
        }
    }
}
