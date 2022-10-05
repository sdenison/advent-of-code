namespace Aoc.Domain.Compute.Instructions.InstructionTypes
{
    public abstract class MathInstruction : IInstruction
    {
        public int Length => 4;
        public bool PassByReferenceParameter1 { get; protected set; }
        public bool PassByReferenceParameter2 { get; protected set; }
        public abstract int ExecuteOperation(int parameter1, int parameter2);

        public MathInstruction(bool passByReferenceParameter1, bool passByReferenceParameter2)
        {
            PassByReferenceParameter1 = passByReferenceParameter1;
            PassByReferenceParameter2 = passByReferenceParameter2;
        }
    }
}
