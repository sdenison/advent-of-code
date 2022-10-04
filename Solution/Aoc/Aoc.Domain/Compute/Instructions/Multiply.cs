namespace Aoc.Domain.Compute.Instructions
{
    public class Multiply : IInstruction
    {
        public Opcodes Opcode => Opcodes.Multiply;

        public int Length => 4;

        public int ExecuteInstruction(int operand1, int operand2)
        {
            return operand1 * operand2;
        }
    }
}
