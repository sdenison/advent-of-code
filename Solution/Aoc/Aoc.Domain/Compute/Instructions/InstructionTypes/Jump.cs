namespace Aoc.Domain.Compute.Instructions.InstructionTypes
{
    internal abstract class Jump : Instruction
    {
        public override int Length => 3;
        
        internal Jump(int opcode) : base(opcode)
        {
        }

        public abstract bool ShouldJump(int parameter);
    }
}
