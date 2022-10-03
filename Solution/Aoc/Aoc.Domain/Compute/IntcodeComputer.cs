using System;

namespace Aoc.Domain.Compute
{
    public class IntcodeComputer
    {
        private int[] WorkingMemory = { };

        public int[] RunProgram(int[] program)
        {
            Array.Resize(ref WorkingMemory, program.Length);
            program.CopyTo(WorkingMemory, 0);
            RunCommand( 0);
            return WorkingMemory;
        }

        private void RunCommand(int programPosition)
        {
            if (WorkingMemory[programPosition] == 1)
            {
                //This is an add command
                //The next two items in the list will be the pointers to the values to add.
                var operand1 = WorkingMemory[programPosition + 1];
                var operand2 = WorkingMemory[programPosition + 2];
                var destination = WorkingMemory[programPosition + 3];
                WorkingMemory[destination] = operand1 + operand2;
            }
            else if (WorkingMemory[programPosition] == 2)
            {
                //This is an multiply command
                //The next two items in the list will be the pointers to the values to add.
                var operand1 = WorkingMemory[programPosition + 1];
                var operand2 = WorkingMemory[programPosition + 2];
                var destination = WorkingMemory[programPosition + 3];
                WorkingMemory[destination] = operand1 * operand2;
            }
        }
    }
}
