namespace Aoc.Domain.Compute.Instructions
{
    public class Put : IOperateOnInputInstruction
    {
        public int Length => 2;
        public int Input { get; set; }
        public void DoOperation()
        {
        }
    }
}
