using System;
using System.Collections.Generic;

namespace Aoc.Domain.Compute.Instructions
{
    internal class JumpIfFalse : IInstruction
    {
        public int Length => throw new NotImplementedException();

        public List<ParameterMode> ParameterModes => throw new NotImplementedException();

        internal JumpIfFalse(int opcodes)
        {

        }
    }
}
