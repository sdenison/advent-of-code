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
            var passByRefParameter1 = GetPassByReferenceParameter1(rawOpcode);
            var passByRefParameter2 = GetPassByReferenceParameter2(rawOpcode);
            
            Opcodes opcode = (Opcodes) (rawOpcode % 100);
            IInstruction instruction;
            switch (opcode)
            {
                case Opcodes.Halt:
                    instruction = new Halt();
                    break;
                case Opcodes.Add:
                    instruction = new Add(passByRefParameter1, passByRefParameter2);
                    break;
                case Opcodes.Multiply:
                    instruction = new Multiply(passByRefParameter1, passByRefParameter2);
                    break;
                case Opcodes.Put:
                    instruction = new Put(passByRefParameter1);
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
                default: throw new InvalidIntcodeProgram($"Unknown instruction {instruction}");
            }
            return InstructionPointer + instruction.Length;
        }

        private void ExecuteInstruction(MathInstruction instruction)
        {
            int parameter1;
            if (instruction.PassByReferenceParameter1)
                parameter1 = Memory[Memory[InstructionPointer + 1]];
            else
                parameter1 = Memory[InstructionPointer + 1];
            int parameter2;
            if (instruction.PassByReferenceParameter2)
                parameter2 = Memory[Memory[InstructionPointer + 2]];
            else
                parameter2 = Memory[InstructionPointer + 2];
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
            if (instruction.PassByReferenceParameter1)
                Memory[Memory[InstructionPointer + 1]] = Input.Value;
            else
                Memory[InstructionPointer + 1] = Input.Value;
        }

        private void CreateWorkingMemory(int[] program)
        {
            //Don't surprise the user and make changes to the incoming program
            Array.Resize(ref Memory, program.Length);
            program.CopyTo(Memory, 0);
        }

        private bool GetPassByReferenceParameter1(int opcode)
        {
            return !GetBoolInDigit(opcode, 2);
        }

        private bool GetPassByReferenceParameter2(int opcode)
        {
            return !GetBoolInDigit(opcode, 3);
        }

        private bool GetBoolInDigit(int opcode, int digit)
        {
            return Convert.ToBoolean(((opcode /= (int) Math.Pow(10, digit))) % 10);
        }
    }
}
