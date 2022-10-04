namespace Aoc.Domain.Compute.Instructions
{
    public class Multiply : IInstruction
    {
        public int Length => 4;
        public int ExecuteOperation(int parameter1, int parameter2)
        {
            return parameter1 * parameter2;
        }
    }
}
