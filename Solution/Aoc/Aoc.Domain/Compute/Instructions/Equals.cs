using System;
using System.Collections.Generic;

namespace Aoc.Domain.Compute.Instructions
{
    internal class Equals : IInstruction
    {
        public int Length => throw new NotImplementedException();

        public List<ParameterMode> ParameterModes => throw new NotImplementedException();

        internal Equals(int opcodes)
        {

        }
    }
}
