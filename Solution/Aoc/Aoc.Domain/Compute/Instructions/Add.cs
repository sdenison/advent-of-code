using Aoc.Domain.Compute.Instructions.InstructionTypes;

namespace Aoc.Domain.Compute.Instructions
{
    public class Add : MathInstruction 
    {
        public Add(bool passByReferenceParameter1, bool passByReferenceParameter2) : base(passByReferenceParameter1, passByReferenceParameter2)
        {
        }

        public override int ExecuteOperation(int parameter1, int parameter2)
        {
            return parameter1 + parameter2;
        }
    }
}
