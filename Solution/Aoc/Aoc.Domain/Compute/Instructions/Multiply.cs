using Aoc.Domain.Compute.Instructions.InstructionTypes;

namespace Aoc.Domain.Compute.Instructions
{
    public class Multiply : Math
    {
        public Multiply(int opcode) : base(opcode)
        {
        }

        public override int ExecuteOperation(int parameter1, int parameter2)
        {
            return parameter1 * parameter2;
        }
    }
}
