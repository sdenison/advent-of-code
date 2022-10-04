namespace Aoc.Domain.Compute.Instructions
{
    public class Halt : IInstruction
    {
        public int Length => 1;
        public int ExecuteOperation(int parameter1, int parameter2)
        {
            throw new System.NotImplementedException();
        }
    }
}
