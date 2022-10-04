namespace Aoc.Domain.Compute
{
    public interface IInstruction
    {
        int Length { get; }
    }

    public interface IMathInstruction : IInstruction
    {
        public bool PassByReferenceParameter1 { get; }
        public bool PassByReferenceParameter2 { get; }
        int ExecuteOperation(int parameter1, int parameter2);
    }

    public interface IOperateOnInputInstruction : IInstruction
    {
        public int Input { get; set; }
        public void DoOperation()
        {
        }
    }
}
