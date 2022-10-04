namespace Aoc.Domain.Compute.Instructions
{
    public interface IInstruction
    {
        Opcodes Opcode { get; }
        int Length { get; }
        int ExecuteInstruction(int operand1, int operand2);
    }
}
