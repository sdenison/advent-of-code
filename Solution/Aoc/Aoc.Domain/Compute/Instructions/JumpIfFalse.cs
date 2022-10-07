using System;
using System.Collections.Generic;

namespace Aoc.Domain.Compute.Instructions
{
    internal class JumpIfFalse : IInstruction
    {
        public int Length => 3;
        public List<ParameterMode> ParameterModes { get; }

        internal JumpIfFalse(int opcode)
        {
            ParameterModes = new List<ParameterMode>();
            ParameterModes.Add(IInstruction.GetParameterMode(opcode, 2));
            ParameterModes.Add(IInstruction.GetParameterMode(opcode, 3));
        }
    }
}
