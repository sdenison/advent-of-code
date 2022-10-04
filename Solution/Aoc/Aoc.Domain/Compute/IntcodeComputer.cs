using System;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;
using Aoc.Domain.Compute.Instructions;

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
            while (address < Memory.Length)
            {
                var instruction = GetNextInstruction(address);
                if (instruction.Opcode == Opcodes.Halt)
                    return Memory;
                address = ExecuteInstruction(instruction, address);
            }
            throw new InvalidIntcodeProgram("No halt instruction at end of program");
        }

        private int ExecuteInstruction(IInstruction instruction, int address)
        {
            var operand1 = Memory[Memory[address + 1]];
            var operand2 = Memory[Memory[address + 2]];
            var instructionValue = instruction.ExecuteInstruction(operand1, operand2);
            var destinationAddress = Memory[address + 3];
            Memory[destinationAddress] = instructionValue;
            return address + instruction.Length;
        }

        private IInstruction GetNextInstruction(int opcodeAddress)
        {
            Opcodes opcode = (Opcodes)Memory[opcodeAddress];
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

            if (Memory.Length < opcodeAddress + returnValue.Length)
                throw new InvalidIntcodeProgram("Last instruction is incomplete");
            return returnValue;
        }

    }
}
