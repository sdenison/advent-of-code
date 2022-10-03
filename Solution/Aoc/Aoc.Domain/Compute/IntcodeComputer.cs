using System;

namespace Aoc.Domain.Compute
{
    public class IntcodeComputer
    {
        private int[] Memory;

        public int[] RunProgram(int[] program)
        {
            Array.Resize(ref Memory, program.Length);
            program.CopyTo(Memory, 0);

            var address = 0;
            while (Memory[address] != 99)
            {
                address = RunCommand(address);
            }
            return Memory;
        }

        private int RunCommand(int opcodeAddress)
        {
            var opcode = Memory[opcodeAddress];
            if (opcode == 1)
            {
                //This is an add command
                //The next two items in the list will be the pointers to the values to add.
                var operand1 = Memory[Memory[opcodeAddress + 1]];
                var operand2 = Memory[Memory[opcodeAddress + 2]];
                var destinationAddress = Memory[opcodeAddress + 3];
                Memory[destinationAddress] = operand1 + operand2;
                return opcodeAddress + 4;
            }
            else if (opcode == 2)
            {
                //This is an multiply command
                //The next two items in the list will be the pointers to the values to add.
                var operand1 = Memory[Memory[opcodeAddress + 1]];
                var operand2 = Memory[Memory[opcodeAddress + 2]];
                var destinationAddress = Memory[opcodeAddress + 3];
                Memory[destinationAddress] = operand1 * operand2;
                return opcodeAddress + 4;
            }

            throw new InvalidIntcodeProgram("Unknown operator");
        }
    }
}
