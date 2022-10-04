using System;
using Aoc.Domain.Compute.Instructions;

namespace Aoc.Domain.Compute
{
    public class IntcodeComputer
    {
        private int[] Memory = Array.Empty<int>();

        public int[] RunProgram(int[] program)
        {
            CreateWorkingMemory(program);

            //Main computer logic
            var instructionPointer = 0;
            while (instructionPointer < Memory.Length)
            {
                var instruction = GetNextInstruction(instructionPointer);
                if (instruction is Halt)
                    return Memory;
                instructionPointer = ExecuteInstruction(instruction, instructionPointer);
            }

            //We should always see a halt operation at the end of the program
            throw new InvalidIntcodeProgram("No halt instruction at end of program");
        }

        private void CreateWorkingMemory(int[] program)
        {
            //Don't surprise the user and make changes to the incoming program
            Array.Resize(ref Memory, program.Length);
            program.CopyTo(Memory, 0);
        }

        private int ExecuteInstruction(IInstruction instruction, int instructionPointer)
        { 
            var parameter1 = Memory[Memory[instructionPointer + 1]];
            var parameter2 = Memory[Memory[instructionPointer + 2]];
            var instructionValue = instruction.ExecuteOperation(parameter1, parameter2);
            var destinationAddress = Memory[instructionPointer + 3];
            Memory[destinationAddress] = instructionValue;
            return instructionPointer + instruction.Length;
        }

        private IInstruction GetNextInstruction(int instructionPointer)
        {
            Opcodes opcode = (Opcodes)Memory[instructionPointer];
            IInstruction instruction;
            switch (opcode)
            {
                case Opcodes.Halt:
                    instruction = new Halt();
                    break;
                case Opcodes.Add:
                    instruction = new Add();
                    break;
                case Opcodes.Multiply:
                    instruction = new Multiply();
                    break;
                default:
                    throw new InvalidIntcodeProgram($"Opcode {opcode} unknown");
            }
            if (Memory.Length < instructionPointer + instruction.Length)
                throw new InvalidIntcodeProgram("Last instruction is incomplete");
            return instruction;
        }
    }
}
