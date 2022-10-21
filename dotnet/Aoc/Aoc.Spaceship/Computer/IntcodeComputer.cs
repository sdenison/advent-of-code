using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aoc.Spaceship.Computer.Instructions;

namespace Aoc.Spaceship.Computer
{
    public class IntcodeComputer : IInputSource
    {
        private int[]? _memory;
        private List<int> _input;
        private int _inputCounter;
        private int _instructionPointer;
        //Keeps track of how many times we've asked parent computer for input
        private int _outputCounter = 0;
        public IInputSource InputPort { get; set; }
        public string Name { get; set; }
        public List<int>? Output { get; set; }

        public IntcodeComputer() : this(new int[] {})
        {
        }

        public IntcodeComputer(int[] input)
        {
            _input = input.ToList();
        }

        public int[] RunProgram(int[] program)
        {
            return RunProgram(program, new int[]{});
        }

        public int[] RunProgram(int[] program, int input)
        {
            return RunProgram(program, new [] {input});
        }

        public int[] RunProgram(int[] program, int[] input)
        {
            return RunProgramAsync(program, input).Result;
        }

        public async Task<int[]> RunProgramAsync(int[] program, int[] input)
        {
            Initialize(program, input.ToList());

            //Main computer logic
            _instructionPointer = 0;
            while (_instructionPointer < _memory.Length)
            {
                var instruction = GetNextInstruction();
                if (instruction is Halt)
                    return _memory;
                _instructionPointer = await ExecuteInstructionAsync(instruction);
            }

            //We should always see a halt operation at the end of the program
            throw new InvalidIntcodeProgram("No halt instruction at end of program");
        }

        private Instruction GetNextInstruction()
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
                    instruction = new Instructions.Math(rawOpcode, mathOperation: (x, y) => x + y);
                    break;
                case Opcodes.Multiply:
                    instruction = new Instructions.Math(rawOpcode, mathOperation: (x, y) => x * y);
                    break;
                default:
                    throw new InvalidIntcodeProgram($"Opcode {opcode} unknown");
            }
            if (_memory.Length < _instructionPointer + instruction.Length)
                throw new InvalidIntcodeProgram("Last instruction is incomplete");
            return instruction;
        }

        private async Task<int> ExecuteInstructionAsync(Instruction instruction)
        {
            switch (instruction)
            {
                case Instructions.Math mathInstruction:
                    ExecuteInstruction(mathInstruction);
                    break;
                case Display displayInstruction:
                    ExecuteInstruction(displayInstruction);
                    break;
                case Put putInstruction:
                    await ExecuteInstructionAsync(putInstruction);
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

        private void ExecuteInstruction(Instructions.Math instruction)
        {
            int parameter1 = GetParameterValue(instruction, 1);
            int parameter2 = GetParameterValue(instruction, 2);
            var instructionValue = instruction.MathOperation(parameter1, parameter2);
            var destinationAddress = GetParameterValue(instruction, 3);
            _memory[destinationAddress] = instructionValue;
        }

        private void ExecuteInstruction(Display instruction)
        {
            var outputValue = GetParameterValue(instruction, 1);
            Output.Add(outputValue);
        }

        private async Task ExecuteInstructionAsync(Put instruction)
        {
            var input = await GetInput();
            if (instruction.ParameterModes[0] == ParameterMode.Immediate)
                _memory[_instructionPointer + 1] = input;
            else
                _memory[_memory[_instructionPointer + 1]] = input;
        }

        //Allows the computer to act as input for another computer
        public async Task<int> GetInput(int outputCounter)
        {
            while (Output == null || Output.Count - 1 < outputCounter)
            {
                await Task.Delay(1);
            }
            return Output[outputCounter];
        }

        private async Task<int> GetInput()
        {
            if (_inputCounter > _input.Count - 1 && !(InputPort is null))
                return await GetFromInputPort();
            return GetFromInitialInput();
        }

        private async Task<int> GetFromInputPort()
        {
            //Getting input from whatever we have plugged in to the input port
            var inputValue = await InputPort.GetInput(_outputCounter);
            _outputCounter++;
            return inputValue;
        }

        private int GetFromInitialInput()
        {
            //This is input that was given at the beginning of the run
            var inputValue = _input[_inputCounter];
            _inputCounter++;
            return inputValue;
        }

        private int GetParameterValue(Instruction instruction, int parameterPosition)
        {
            if (instruction.ParameterModes[parameterPosition - 1] == ParameterMode.Immediate)
                return _memory[_instructionPointer + parameterPosition];
            return _memory[_memory[_instructionPointer + parameterPosition]];
        }

        private void Initialize(int[] program, List<int> input)
        {
            //Make sure we don't use input from previous runs
            _input = input;
            _inputCounter = 0;
            //Make sure we don't have output from previous program runs
            Output = new List<int>();
            //Don't surprise the user and make changes to the incoming program
            _memory = Array.Empty<int>();
            Array.Resize(ref _memory, program.Length);
            program.CopyTo(_memory, 0);
        }
    }
}
