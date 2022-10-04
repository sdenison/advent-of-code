namespace Aoc.Domain.Compute
{
    public interface IInstruction
    {
        int Length { get; }
    }

    public interface IMathInstruction : IInstruction
    {
        int ExecuteOperation(int parameter1, int parameter2);
    }
}
