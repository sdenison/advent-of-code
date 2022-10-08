using System.Collections.Generic;

namespace Aoc.Domain.Compute.Instructions
{
    public abstract class Instruction
    {
        public abstract int Length { get; }
        public List<ParameterMode> ParameterModes { get; protected set; }

        public static ParameterMode GetParameterMode(int opcode, int positionIndex)
        {
            //Assume zero based index so if we want the hundreds column then positionIndex would be 2
            return (ParameterMode) (((opcode / (int) System.Math.Pow(10, positionIndex))) % 10);
        }

        public Instruction(int opcode)
        {
            ParameterModes = new List<ParameterMode>
            {
                GetParameterMode(opcode, 2),
                GetParameterMode(opcode, 3)
            };
        }

        public Instruction()
        {
        }
    }
}
