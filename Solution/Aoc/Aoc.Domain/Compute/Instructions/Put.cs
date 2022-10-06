namespace Aoc.Domain.Compute.Instructions
{
    public class Put : IInstruction
    {
        public int Length => 2;
        public bool PassByReferenceParameter1 { get; }

        public Put(int opcode)
        {
            PassByReferenceParameter1 = !IInstruction.GetBoolInDigit(opcode, 2);
        }
    }
}
