namespace Aoc.Domain.Compute.Instructions
{
    public class Add : IInstruction
    {
        public Opcodes Opcode => Opcodes.Add;

        public int Length => 4;

        public int ExecuteInstruction(int operand1, int operand2)
        {
            return operand1 + operand2;
        }
    }
}
