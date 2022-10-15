using System;

namespace Aoc.Spaceship.Computer.Instructions
{
    internal class Math : Instruction
    {
        internal Func<int, int, int> MathOperation { get; }
        internal override int Length => 4;

        internal Math(int opcode, Func<int, int, int> mathOperation) : base(opcode)
        {
            MathOperation = mathOperation;
            ParameterModes.Add(ParameterMode.Immediate);
        }
    }
}
