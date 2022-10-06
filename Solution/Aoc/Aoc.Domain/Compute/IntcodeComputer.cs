using System;
using System.Collections.Generic;
using Aoc.Domain.Compute.Instructions;
using Aoc.Domain.Compute.Instructions.InstructionTypes;

namespace Aoc.Domain.Compute
{
    public class IntcodeComputer
    {
        public List<int> Output { get; set;  }
        private int[] Memory = Array.Empty<int>();
        private int? Input = 0;
        private int InstructionPointer;

        public IntcodeComputer()
        {
            Output = new List<int>();
        }

        public int[] RunProgram(int[] program, int? input = null)
        {
            Input = input;
            CreateWorkingMemory(program);

            //Main computer logic
            InstructionPointer = 0;
            while (InstructionPointer < Memory.Length)
            {
                var instruction = GetNextInstruction();
                if (instruction is Halt)
                    return Memory;
                InstructionPointer = ExecuteInstruction(instruction);
            }

            //We should always see a halt operation at the end of the program
            throw new InvalidIntcodeProgram("No halt instruction at end of program");
        }

        private IInstruction GetNextInstruction()
        {
            var rawOpcode = Memory[InstructionPointer];
            Opcodes opcode = (Opcodes) (rawOpcode % 100);
            IInstruction instruction;
            switch (opcode)
            {
                case Opcodes.Halt:
                    instruction = new Halt();
                    break;
                case Opcodes.Add:
                    instruction = new Add(rawOpcode);
                    break;
                case Opcodes.Multiply:
                    instruction = new Multiply(rawOpcode);
                    break;
                case Opcodes.Put:
                    instruction = new Put(rawOpcode);
                    break;
                case Opcodes.Display:
                    instruction = new Display();
                    break;
                default:
                    throw new InvalidIntcodeProgram($"Opcode {opcode} unknown");
            }
            if (Memory.Length < InstructionPointer + instruction.Length)
                throw new InvalidIntcodeProgram("Last instruction is incomplete");
            return instruction;
        }

        private int ExecuteInstruction(IInstruction instruction)
        {
            switch (instruction)
            {
                case MathInstruction mathInstruction:
                    ExecuteInstruction(mathInstruction);
                    break;
                case Display displayInstruction:
                    ExecuteInstruction(displayInstruction);
                    break;
                case Put putInstruction:
                    ExecuteInstruction(putInstruction);
                    break;
                default: 
                    throw new InvalidIntcodeProgram($"Unknown instruction {instruction}");
            }
            return InstructionPointer + instruction.Length;
        }

        private void ExecuteInstruction(MathInstruction instruction)
        {
            int parameter1 = GetParameterValue(instruction, 1);
            int parameter2 = GetParameterValue(instruction, 2);
            var instructionValue = instruction.ExecuteOperation(parameter1, parameter2);
            var destinationAddress = Memory[InstructionPointer + 3];
            Memory[destinationAddress] = instructionValue;
        }

        private void ExecuteInstruction(Display instruction)
        {
            Output.Add(Memory[Memory[InstructionPointer + 1]]);
        }

        private void ExecuteInstruction(Put instruction)
        {
            if (!Input.HasValue)
                throw new InvalidIntcodeProgram("This program expects input from user and none was given");
            if (instruction.ParameterModes[0] == ParameterMode.Immediate)
                Memory[InstructionPointer + 1] = Input.Value;
            else
                Memory[Memory[InstructionPointer + 1]] = Input.Value;
        }

        private int GetParameterValue(IInstruction instruction, int parameterPosition)
        {
            if (instruction.ParameterModes[parameterPosition - 1] == ParameterMode.Immediate)
                    return Memory[InstructionPointer + parameterPosition];
            return Memory[Memory[InstructionPointer + parameterPosition]];
        }

        private void CreateWorkingMemory(int[] program)
        {
            //Don't surprise the user and make changes to the incoming program
            Array.Resize(ref Memory, program.Length);
            program.CopyTo(Memory, 0);
        }
    }
}
