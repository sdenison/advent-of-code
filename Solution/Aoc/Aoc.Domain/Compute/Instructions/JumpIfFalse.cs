﻿using Aoc.Domain.Compute.Instructions.InstructionTypes;

namespace Aoc.Domain.Compute.Instructions
{
    internal class JumpIfFalse : Jump
    {
        public JumpIfFalse(int opcode) : base(opcode)
        {
        }

        public override bool ShouldJump(int parameter)
        {
            return parameter == 0;
        }
    }
}
