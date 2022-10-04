using System;
using Aoc.Domain.Compute.Instructions; 

namespace Aoc.Domain.Compute
{
    public class IntcodeComputer
    {
        private int[] Memory = Array.Empty<int>();

        public int[] RunProgram(int[] program)
        {
            Array.Resize(ref Memory, program.Length);
            program.CopyTo(Memory, 0);
            var instructionPointer = 0;
            while (instructionPointer < Memory.Length)
            {
                var instruction = GetNextInstruction(instructionPointer);
                if (instruction.Opcode == Opcodes.Halt)
                    return Memory;
                instructionPointer = ExecuteInstruction(instruction, instructionPointer);
            }
            throw new InvalidIntcodeProgram("No halt instruction at end of program");
        }

        private int ExecuteInstruction(IInstruction instruction, int instructionPointer)
        { 
            var parameter1 = Memory[Memory[instructionPointer + 1]];
            var parameter2 = Memory[Memory[instructionPointer + 2]];
            var instructionValue = instruction.ExecuteInstruction(parameter1, parameter2);
            var destinationAddress = Memory[instructionPointer + 3];
            Memory[destinationAddress] = instructionValue;
            return instructionPointer + instruction.Length;
        }

        private IInstruction GetNextInstruction(int instructionPointer)
        {
            Opcodes opcode = (Opcodes)Memory[instructionPointer];
            IInstruction returnValue;
            switch (opcode)
            {
                case Opcodes.Halt:
                    returnValue = new Halt();
                    break;
                case Opcodes.Add:
                    returnValue = new Add();
                    break;
                case Opcodes.Multiply:
                    returnValue = new Multiply();
                    break;
                default:
                    throw new InvalidIntcodeProgram($"Opcode {opcode} unknown");
            }

            if (Memory.Length < instructionPointer + returnValue.Length)
                throw new InvalidIntcodeProgram("Last instruction is incomplete");
            return returnValue;
        }
    }
}
