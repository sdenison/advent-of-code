using System;

namespace Aoc.Spaceship.Computer.Instructions
{
    internal class Jump : Instruction
    {
        internal Func<bool> _jumpIfTrue { get; }
        internal override int Length => 3;

        internal Jump(int opcode, Func<bool> jumpIfTrue) : base(opcode)
        {
            _jumpIfTrue = jumpIfTrue;
        }

        internal bool ShouldJump(int parameter)
        {
            if (_jumpIfTrue())
                return parameter > 0;
            //If we get here we know that it's jump if false
            return parameter == 0;
        }
    }
}
