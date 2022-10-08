namespace Aoc.Domain.Compute.Instructions
{
    public class Put : Instruction
    {
        public override int Length => 2;

        public Put(int opcode) : base(opcode)
        {
        }
    }
}
