namespace Aoc.Domain.Compute.Instructions
{
    public class Put : IOperateOnInputInstruction
    {
        public int Length => 2;
        public int Input { get; set; }
        public bool PassByReferenceParameter1 { get; }

        public Put(bool passByReferenceParameter1)
        {
            PassByReferenceParameter1 = passByReferenceParameter1;
        }

        public void DoOperation()
        {
        }
    }
}
