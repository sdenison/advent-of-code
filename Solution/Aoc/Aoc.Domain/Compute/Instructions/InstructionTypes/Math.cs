namespace Aoc.Domain.Compute.Instructions.InstructionTypes
{
    public abstract class Math : Instruction
    {
        public override int Length => 4;

        public abstract int ExecuteOperation(int parameter1, int parameter2);

        protected Math(int opcode) : base(opcode)
        {
        }
    }
}
