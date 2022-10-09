using System;

namespace Aoc.Domain.Compute.Instructions
{
    public class Math : Instruction
    {
        public Func<int, int, int> MathOperation { get; }
        public override int Length => 4;

        public Math(int opcode, Func<int, int, int> mathOperation) : base(opcode)
        {
            MathOperation = mathOperation;
            ParameterModes.Add(ParameterMode.Immediate);
        }
    }
}
