using System;

namespace Aoc.Spaceship.Computer.Instructions
{
    internal class Compare : Instruction
    {
        internal Func<int, int, bool> CompareFunction;
        internal override int Length => 4;

        internal Compare(int opcode, Func<int, int, bool> compareFunction) : base(opcode)
        {
            CompareFunction = compareFunction;
            ParameterModes.Add(ParameterMode.Immediate);
        }
    }
}
