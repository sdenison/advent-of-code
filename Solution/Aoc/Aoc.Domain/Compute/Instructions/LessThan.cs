using System;
using System.Collections.Generic;

namespace Aoc.Domain.Compute.Instructions
{
    internal class LessThan : IInstruction
    {
        public int Length => throw new NotImplementedException();

        public List<ParameterMode> ParameterModes => throw new NotImplementedException();

        internal LessThan(int opcodes)
        {

        }
    }
}
