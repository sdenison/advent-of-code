using System;
using System.Linq;

namespace Aoc.Domain.Compute
{
    public class IntcodeComputer
    {
        private int[] WorkingMemory = { };

        public int[] RunProgram(int[] program)
        {
            Array.Resize(ref WorkingMemory, program.Length);
            program.CopyTo(WorkingMemory, 0);

            var nextPosition = 0;
            while (WorkingMemory[nextPosition] != 99)
            {
                nextPosition = RunCommand(nextPosition);
            }
            return WorkingMemory;
        }

        private int RunCommand(int programPosition)
        {
            if (WorkingMemory[programPosition] == 1)
            {
                //This is an add command
                //The next two items in the list will be the pointers to the values to add.
                var operand1 = WorkingMemory[WorkingMemory[programPosition + 1]];
                var operand2 = WorkingMemory[WorkingMemory[programPosition + 2]];
                var destination = WorkingMemory[programPosition + 3];
                WorkingMemory[destination] = operand1 + operand2;
                return programPosition + 4;
            }
            else if (WorkingMemory[programPosition] == 2)
            {
                //This is an multiply command
                //The next two items in the list will be the pointers to the values to add.
                var operand1 = WorkingMemory[WorkingMemory[programPosition + 1]];
                var operand2 = WorkingMemory[WorkingMemory[programPosition + 2]];
                var destination = WorkingMemory[programPosition + 3];
                WorkingMemory[destination] = operand1 * operand2;
                return programPosition + 4;
            }

            return programPosition + 4;
        }

    }
}
