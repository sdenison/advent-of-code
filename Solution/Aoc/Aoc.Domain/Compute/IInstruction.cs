using System;

namespace Aoc.Domain.Compute
{
    public interface IInstruction
    {
        int Length { get; }

        static bool GetBoolInDigit(int opcode, int digit)
        {
            return Convert.ToBoolean(((opcode / (int) Math.Pow(10, digit))) % 10);
        }
    }
}
