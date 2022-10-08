from domain.compute.instructions.compare import Compare
from domain.compute.instructions.display import Display
from domain.compute.instructions.halt import Halt
from domain.compute.instructions.jump import Jump
from domain.compute.instructions.math import Math
from domain.compute.instructions.parameter_mode import ParameterMode
from domain.compute.instructions.put import Put
from domain.compute.invalid_intcode_program import InvalidIntcodeProgram
from domain.compute.opcodes import Opcodes


class IntcodeComputer(object):
    def __init__(self):
        self._memory = None
        self._input = 0
        self._instructionPointer = None
        self._Output = None

    @property
    def output(self):
        return self._Output

    def run_program(self, program, userinput):
        self.initialize(program, userinput)
        self._instructionPointer = 0

        # Main computer logic
        while (self._instructionPointer < len(program)):
            instruction = self.get_next_instruction()
            if isinstance(instruction, Halt):
                return self._memory
            self._instructionPointer = self.execute_instruction(instruction)

        raise InvalidIntcodeProgram("No halt instruction at end of program")

    def get_next_instruction(self):
        opcode = self._memory[self._instructionPointer]
        opcodeEnum = Opcodes(opcode % 100)
        match opcodeEnum:
            case Opcodes.Halt:
                return Halt()
            case Opcodes.Display:
                return Display()
            case Opcodes.Put:
                return Put(opcode)
            case Opcodes.JumpIfTrue:
                return Jump(opcode, lambda: True)
            case Opcodes.JumpIfFalse:
                return Jump(opcode, lambda: False)
            case Opcodes.LessThan:
                return Compare(opcode, lambda x, y: x < y)
            case Opcodes.Equals:
                return Compare(opcode, lambda x, y: x == y)
            case Opcodes.Add:
                return Math(opcode, lambda x, y: x + y)
            case Opcodes.Multiply:
                return Math(opcode, lambda x, y: x * y)

    def execute_instruction(self, instruction):
        if isinstance(instruction, Math):
            self.execute_math_instruction(instruction)
        if isinstance(instruction, Display):
            self.execute_display_instruction(instruction)
        if isinstance(instruction, Put):
            self.execute_put_instruction(instruction)
        if isinstance(instruction, Compare):
            self.execute_compare_instruction(instruction)
        if isinstance(instruction, Jump):
            return self.execute_jump_instruction(instruction)
        return self._instructionPointer + instruction.Length

    def execute_jump_instruction(self, instruction):
        parameter1 = self.get_parameter_value(instruction, 1)
        parameter2 = self.get_parameter_value(instruction, 2)
        if instruction.ShouldJump(parameter1):
            return parameter2
        return self._instructionPointer + instruction.Length

    def execute_compare_instruction(self, instruction):
        parameter1 = self.get_parameter_value(instruction, 1)
        parameter2 = self.get_parameter_value(instruction, 2)
        destinationAddress = self._memory[(self._instructionPointer + 3)]
        self._memory[destinationAddress] = (1 if instruction.CompareFunction(parameter1, parameter2) else 0)

    def execute_math_instruction(self, instruction):
        parameter1 = self.get_parameter_value(instruction, 1)
        parameter2 = self.get_parameter_value(instruction, 2)
        instructionValue = instruction.MathOperation(parameter1, parameter2)
        destinationAddress = self._memory[(self._instructionPointer + 3)]
        self._memory[destinationAddress] = instructionValue

    def execute_display_instruction(self, instruction):
        self._Output.append(self._memory[self._memory[(self._instructionPointer + 1)]])

    def execute_put_instruction(self, instruction):
        if (self._input == None):
            raise InvalidIntcodeProgram("This program expects input from user and none was given")
        if (instruction.ParameterModes[0] == ParameterMode.Immediate):
            self._memory[(self._instructionPointer + 1)] = self._input
        else:
            self._memory[self._memory[(self._instructionPointer + 1)]] = self._input

    def get_parameter_value(self, instruction, parameterPosition):
        if (instruction.ParameterModes[(parameterPosition - 1)] == ParameterMode.Immediate):
            return self._memory[(self._instructionPointer + parameterPosition)]
        return self._memory[self._memory[(self._instructionPointer + parameterPosition)]]

    def initialize(self, program, userInput):
        self._input = userInput
        # Don't surprise the user and change the incoming program
        self._Output = []
        self._memory = program.copy()
