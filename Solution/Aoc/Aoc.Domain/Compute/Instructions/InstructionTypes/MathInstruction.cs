namespace Aoc.Domain.Compute.Instructions.InstructionTypes
{
    public abstract class MathInstruction : IInstruction
    {
        public int Length => 4;
        public bool PassByReferenceParameter1 { get; protected set; }
        public bool PassByReferenceParameter2 { get; protected set; }
        public abstract int ExecuteOperation(int parameter1, int parameter2);

        protected MathInstruction(int opcode)
        {
            PassByReferenceParameter1 = !IInstruction.GetBoolInDigit(opcode, 2);
            PassByReferenceParameter2 = !IInstruction.GetBoolInDigit(opcode, 3);
        }
    }
}
