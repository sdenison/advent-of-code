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
            int parameter1;
            switch (instruction)
            {
                case IMathInstruction mathInstruction:
                    if (mathInstruction.PassByReferenceParameter1)
                        parameter1 = Memory[Memory[instructionPointer + 1]];
                    else
                        parameter1 = Memory[instructionPointer + 1];
                    int parameter2;
                    if (mathInstruction.PassByReferenceParameter2)
                        parameter2 = Memory[Memory[instructionPointer + 2]];
                    else
                        parameter2 = Memory[instructionPointer + 2];
                    var instructionValue = mathInstruction.ExecuteOperation(parameter1, parameter2);
                    var destinationAddress = Memory[instructionPointer + 3];
                    Memory[destinationAddress] = instructionValue;
                    break;
                case IOperateOnInputInstruction inputInstruction:
                {
                    if (inputInstruction is Display)
                        parameter1 = Memory[Memory[instructionPointer + 1]];
                    if (inputInstruction is Put)
                    {
                        var put = (Put) inputInstruction;
                        if (put.PassByReferenceParameter1)
                            parameter1 = Memory[Memory[instructionPointer + 1]];
                        else
                            parameter1 = Memory[instructionPointer + 1];
                    }
                    var operationInstruction = inputInstruction;
                    operationInstruction.Input = 9999;
                    operationInstruction.DoOperation();
                    break;
                }
            }
            return instructionPointer + instruction.Length;
        }

        private IInstruction GetNextInstruction(int instructionPointer)
        {
            var rawOpcode = Memory[instructionPointer];
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
            if (Memory.Length < instructionPointer + instruction.Length)
                throw new InvalidIntcodeProgram("Last instruction is incomplete");
            return instruction;
        }

        private bool GetPassByReferenceParameter1(int rawOpcode)
        {
            var hundreds = (rawOpcode /= 100) % 10;
            return !Convert.ToBoolean(hundreds);
        }

        private bool GetPassByReferenceParameter2(int rawOpcode)
        {
            var hundreds = (rawOpcode /= 1000) % 10;
            return !Convert.ToBoolean(hundreds);
        }
    }
}
