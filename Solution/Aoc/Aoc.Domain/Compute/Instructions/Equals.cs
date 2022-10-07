using Aoc.Domain.Compute.Instructions.InstructionTypes;

namespace Aoc.Domain.Compute.Instructions
{
    internal class Equals : Compare
    {
        internal Equals(int opcode) : base(opcode)
        {
        }

        internal override bool DoComparison(int parameter1, int parameter2)
        {
            return parameter1 == parameter2;
        }
    }
}
