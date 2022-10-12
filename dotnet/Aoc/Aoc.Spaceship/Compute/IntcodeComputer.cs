using System;
using System.Collections.Generic;
using System.Linq;
using Aoc.Spaceship.Compute.Instructions;
using Math = Aoc.Spaceship.Compute.Instructions.Math;

namespace Aoc.Spaceship.Compute
{
    public class IntcodeComputer {
        private int[]? _memory;
        private List<int> _input;
        private int _instructionPointer;
        public List<int>? Output { get; set;  }

        public int[] RunProgram(int[] program, int? input = null)
        {
            return RunProgram(program, new[] {input.Value});
            Initialize(program, input);

            //Main computer logic
            _instructionPointer = 0;
            while (_instructionPointer < _memory.Length)
            {
                var instruction = GetNextInstruction();
                if (instruction is Halt)
                    return _memory;
                _instructionPointer = ExecuteInstruction(instruction);
            }

            //We should always see a halt operation at the end of the program
            throw new InvalidIntcodeProgram("No halt instruction at end of program");
        }

        public int[] RunProgram(int[] program, int[] input)
        {
            Initialize(program, input.ToList());

            //Main computer logic
            _instructionPointer = 0;
            while (_instructionPointer < _memory.Length)
            {
                var instruction = GetNextInstruction();
                if (instruction is Halt)
                    return _memory;
                _instructionPointer = ExecuteInstruction(instruction);
            }

            //We should always see a halt operation at the end of the program
            throw new InvalidIntcodeProgram("No halt instruction at end of program");
        }
l  h     private Instruction GetNextInstruction()
        {
            var rawOpcode = _memory[_instructionPointer];
            Opcodes opcode = (Opcodes) (rawOpcode % 100);
            Instruction instruction;
            switch (opcode)
            {
                case Opcodes.Halt:
                    instruction = new Halt();
                    break;
                case Opcodes.Display:
                    instruction = new Display();
                    break;
                case Opcodes.Put:
                    instruction = new Put(rawOpcode);
                    break;
                case Opcodes.JumpIfTrue:
                    instruction = new Jump(rawOpcode,() => true);
                    break;
                case Opcodes.JumpIfFalse:
                    instruction = new Jump(rawOpcode,() => false);
                    break;
                case Opcodes.LessThan:
                    instruction = new Compare(rawOpcode, compareFunction: (x, y) => x < y);
                    break;
                case Opcodes.Equals:
                    instruction = new Compare(rawOpcode, compareFunction: (x, y) => x == y);
                    break;
                case Opcodes.Add:
                    instruction = new Math(rawOpcode, mathOperation: (x, y) => x + y);
                    break;
                case Opcodes.Multiply:
                    instruction = new Math(rawOpcode, mathOperation: (x, y) => x * y);
                    break;
                default:
                    throw new InvalidIntcodeProgram($"Opcode {opcode} unknown");
            }
            if (_memory.Length < _instructionPointer + instruction.Length)
                throw new InvalidIntcodeProgram("Last instruction is incomplete");
            return instruction;
        }

        private int ExecuteInstruction(Instruction instruction)
        {
            switch (instruction)
            {
                case Math mathInstruction:
                    ExecuteInstruction(mathInstruction);
                    break;
                case Display displayInstruction:
                    ExecuteInstruction(displayInstruction);
                    break;
                case Put putInstruction:
                    ExecuteInstruction(putInstruction);
                    break;
                case Jump jumpInstruction:
                    return ExecuteInstruction(jumpInstruction);
                case Compare compareInstruction:
                    ExecuteInstruction(compareInstruction);
                    break;
                default: 
                    throw new InvalidIntcodeProgram($"Unknown instruction {instruction}");
            }
            return _instructionPointer + instruction.Length;
        }

        private int ExecuteInstruction(Jump instruction)
        {
            int parameter1 = GetParameterValue(instruction, 1);
            int parameter2 = GetParameterValue(instruction, 2);
            if (instruction.ShouldJump(parameter1))
                return parameter2;
            return _instructionPointer + instruction.Length;
        }

        private void ExecuteInstruction(Compare instruction)
        {
            int parameter1 = GetParameterValue(instruction, 1);
            int parameter2 = GetParameterValue(instruction, 2);
            var destinationAddress = GetParameterValue(instruction, 3);
            _memory[destinationAddress] = instruction.CompareFunction(parameter1, parameter2) ? 1 : 0;
        }

        private void ExecuteInstruction(Math instruction)
        {
            int parameter1 = GetParameterValue(instruction, 1);
            int parameter2 = GetParameterValue(instruction, 2);
            var instructionValue = instruction.MathOperation(parameter1, parameter2);
            var destinationAddress = GetParameterValue(instruction, 3);
            _memory[destinationAddress] = instructionValue;
        }

        private void ExecuteInstruction(Display instruction)
        {
            Output.Add(GetParameterValue(instruction, 1));
        }

        private void ExecuteInstruction(Put instruction)
        {
            if (!_input.HasValue)
                throw new InvalidIntcodeProgram("This program expects input from user and none was given");
            if (instruction.ParameterModes[0] == ParameterMode.Immediate)
                _memory[_instructionPointer + 1] = _input.Value;
            else
                _memory[_memory[_instructionPointer + 1]] = _input.Value;
        }

        private int GetParameterValue(Instruction instruction, int parameterPosition)
        {
            if (instruction.ParameterModes[parameterPosition - 1] == ParameterMode.Immediate)
                return _memory[_instructionPointer + parameterPosition];
            return _memory[_memory[_instructionPointer + parameterPosition]];
        }

        private void Initialize(int[] program, int[] input)
        {
            //Make sure we don't use input from previous runs
            _input = input;
            //Make sure we don't have output from previous program runs
            Output = new List<int>();
            //Don't surprise the user and make changes to the incoming program
            _memory = Array.Empty<int>();
            Array.Resize(ref _memory, program.Length);
            program.CopyTo(_memory, 0);
        }
    }
}
