namespace Aoc.Domain.Compute.Instructions
{
    public class Add : IInstruction
    {
        public Opcodes Opcode => Opcodes.Add;

        public int Length => 4;

        public int ExecuteInstruction(int parameter1, int parameter2)
        {
            return parameter1 + parameter2;
        }
    }
}
