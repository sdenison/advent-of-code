using System;
using System.Collections.Generic;
using Aoc.Domain.Compute.Instructions;

namespace Aoc.Domain.Compute
{
    public interface IInstruction
    {
        int Length { get; }
        List<ParameterMode> ParameterModes { get; }

        static ParameterMode GetParameterMode(int opcode, int digit)
        {
            return (ParameterMode) (((opcode / (int) Math.Pow(10, digit))) % 10);
        }
    }
}
