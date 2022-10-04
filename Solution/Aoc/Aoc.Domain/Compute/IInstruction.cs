namespace Aoc.Domain.Compute
{
    public interface IInstruction
    {
        int Length { get; }
        int ExecuteOperation(int parameter1, int parameter2);
    }
}
