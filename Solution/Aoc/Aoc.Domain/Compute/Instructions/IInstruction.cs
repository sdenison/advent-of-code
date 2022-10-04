namespace Aoc.Domain.Compute.Instructions
{
    public interface IInstruction
    {
        Opcodes Opcode { get; }
        int Length { get; }
        int ExecuteInstruction(int parameter1, int parameter2);
    }
}
