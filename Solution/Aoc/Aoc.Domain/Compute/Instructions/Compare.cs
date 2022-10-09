using System;

namespace Aoc.Domain.Compute.Instructions
{
    internal class Compare : Instruction
    {
        public Func<int, int, bool> CompareFunction;
        public override int Length => 4;

        internal Compare(int opcode, Func<int, int, bool> compareFunction) : base(opcode)
        {
            CompareFunction = compareFunction;
            ParameterModes.Add(ParameterMode.Immediate);
        }
    }
}
