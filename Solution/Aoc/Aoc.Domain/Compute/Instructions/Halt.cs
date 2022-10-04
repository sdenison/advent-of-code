namespace Aoc.Domain.Compute.Instructions
{
    public class Halt : IInstruction
    {
        public Opcodes Opcode => Opcodes.Halt;
        public int Length => 1;
        public int ExecuteInstruction(int parameter1, int parameter2)
        {
            throw new System.NotImplementedException();
        }
    }
}
