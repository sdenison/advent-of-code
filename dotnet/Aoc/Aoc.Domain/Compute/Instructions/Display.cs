﻿using System.Collections.Generic;

namespace Aoc.Domain.Compute.Instructions
{
    internal class Display : Instruction
    {
        internal override int Length => 2;

        internal Display()
        {
            ParameterModes = new List<ParameterMode> {ParameterMode.Reference};
        }
    }
}
