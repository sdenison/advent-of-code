using System;
using System.Collections.Generic;

namespace Aoc.Domain.Compute.Instructions
{
    internal class LessThan : IInstruction
    {
        public int Length => 4;

        public List<ParameterMode> ParameterModes { get; }

        internal LessThan(int opcode)
        {
            ParameterModes = new List<ParameterMode>();
            ParameterModes.Add(IInstruction.GetParameterMode(opcode, 2));
            ParameterModes.Add(IInstruction.GetParameterMode(opcode, 3));
        }
    }
}
