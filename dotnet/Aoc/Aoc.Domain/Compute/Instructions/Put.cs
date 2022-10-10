namespace Aoc.Domain.Compute.Instructions
{
    internal class Put : Instruction
    {
        internal override int Length => 2;

        internal Put(int opcode) : base(opcode)
        {
        }
    }
}
