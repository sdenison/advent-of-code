using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Aoc.Domain.Compute.Instructions
{
    public class Halt : IInstruction
    {
        public Opcodes Opcode => Opcodes.Halt;
        public int Length => 1;
    }
}
