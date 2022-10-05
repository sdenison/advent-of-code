namespace Aoc.Domain.Compute.Instructions
{
    public class Put : IInstruction
    {
        public int Length => 2;
        public bool PassByReferenceParameter1 { get; }

        public Put(bool passByReferenceParameter1)
        {
            PassByReferenceParameter1 = passByReferenceParameter1;
        }
    }
}
