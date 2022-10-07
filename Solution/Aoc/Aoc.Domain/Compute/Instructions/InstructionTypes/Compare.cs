namespace Aoc.Domain.Compute.Instructions.InstructionTypes
{
    internal abstract class Compare : Instruction
    {
        public override int Length => 4;

        internal Compare(int opcode) : base(opcode)
        {
        }

        internal abstract bool DoComparison(int parameter1, int parameter2);
    }
}
