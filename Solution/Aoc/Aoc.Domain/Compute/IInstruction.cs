using System;
using System.Collections.Generic;
using Aoc.Domain.Compute.Instructions;

namespace Aoc.Domain.Compute
{
    public interface IInstruction
    {
        int Length { get; }
        List<ParameterMode> ParameterModes { get; }

        static ParameterMode GetParameterMode(int opcode, int positionIndex)
        {
            //Assume zero based index so if we want the hundreds column then positionIndex would be 2
            return (ParameterMode) (((opcode / (int) Math.Pow(10, positionIndex))) % 10);
        }
    }
}
