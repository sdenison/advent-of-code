namespace Aoc.Domain.Compute.Instructions
{
    public class Multiply : IMathInstruction
    {
        public bool PassByReferenceParameter1 { get; }
        public bool PassByReferenceParameter2 { get; }
        public int Length => 4;

        public Multiply(bool passByReferenceParameter1, bool passByReferenceParameter2)
        {
            PassByReferenceParameter1 = passByReferenceParameter1;
            PassByReferenceParameter2 = passByReferenceParameter2;
        }

        public int ExecuteOperation(int parameter1, int parameter2)
        {
            return parameter1 * parameter2;
        }
    }
}
