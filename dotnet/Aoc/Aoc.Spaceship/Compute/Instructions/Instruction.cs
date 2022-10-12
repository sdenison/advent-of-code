using System.Collections.Generic;

namespace Aoc.Spaceship.Compute.Instructions
{
    internal abstract class Instruction
    {
        internal abstract int Length { get; }
        internal List<ParameterMode> ParameterModes { get; set; }

        internal static ParameterMode GetParameterMode(int opcode, int positionIndex)
        {
            //Assume zero based index so if we want the hundreds column then positionIndex would be 2
            return (ParameterMode) (((opcode / (int) System.Math.Pow(10, positionIndex))) % 10);
        }

        internal Instruction(int opcode)
        {
            ParameterModes = new List<ParameterMode>
            {
                GetParameterMode(opcode, 2),
                GetParameterMode(opcode, 3)
            };
        }

        internal Instruction()
        {
        }
    }
}
