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
    def Output(self):
        return self._Output
    @Output.setter
    def Output(self, value):
        self._Output = value

    def RunProgram(self, program, userinput):
        self.Initialize(program, userinput)
        self._instructionPointer = 0

        #Main computer logic
        while(self._instructionPointer < len(program)):
            instruction = self.GetNextInstruction()
            if isinstance(instruction, Halt):
                return self._memory
            self._instructionPointer = self.ExecuteInstruction(instruction)

        raise InvalidIntcodeProgram("No halt instruction at end of program")

    def GetNextInstruction(self):
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
                return Jump(opcode, lambda x, y: x < y)
            case Opcodes.Equals:
                return Jump(opcode, lambda x, y: x == y)
            case Opcodes.Add:
                return Math(opcode, lambda x, y: x + y)
            case Opcodes.Multiply:
                return Math(opcode, lambda x, y: x * y)

    def ExecuteInstruction(self, instruction):
        if isinstance(instruction, Math):
            self.ExecuteMathInstruction(instruction)
        if isinstance(instruction, Display):
            self.ExecuteDisplayInstruction(instruction)
        if isinstance(instruction, Put):
            self.ExecutePutInstruction(instruction)
        if isinstance(instruction, Compare):
            self.ExecuteCompareInstruction(instruction)
        if isinstance(instruction, Jump):
            return self.ExecuteJumpInstruction(instruction)
        return self._instructionPointer + instruction.Length

    def ExecuteJumpInstruction(self, instruction):
        parameter1 = self.GetParameterValue(instruction, 1)
        parameter2 = self.GetParameterValue(instruction, 2)
        if instruction.ShouldJump(parameter1):
            return parameter2
        return (self._instructionPointer + instruction.Length)

    def ExecuteCompareInstruction(self, instruction):
        parameter1 = self.GetParameterValue(instruction, 1)
        parameter2 = self.GetParameterValue(instruction, 2)
        destinationAddress = self._memory[(self._instructionPointer + 3)]
        self._memory[destinationAddress] = (1 if instruction.CompareFunction(parameter1, parameter2) else 0)

    def ExecuteMathInstruction(self, instruction):
        parameter1 = self.GetParameterValue(instruction, 1)
        parameter2 = self.GetParameterValue(instruction, 2)
        instructionValue = instruction.MathOperation(parameter1, parameter2)
        destinationAddress = self._memory[(self._instructionPointer + 3)]
        self._memory[destinationAddress] = instructionValue

    def ExecuteDisplayInstruction(self, instruction):
        self._Output.append(self._memory[self._memory[(self._instructionPointer + 1)]])

    def ExecutePutInstruction(self, instruction):
        if (self._input == None):
            raise InvalidIntcodeProgram("This program expects input from user and none was given")
        if (instruction.ParameterModes[0] == ParameterMode.Immediate):
            self._memory[(self._instructionPointer + 1)] = self._input
        else:
            self._memory[self._memory[(self._instructionPointer + 1)]] = self._input

    def GetParameterValue(self, instruction, parameterPosition):
        if (instruction.ParameterModes[(parameterPosition - 1)] == ParameterMode.Immediate):
            return self._memory[(self._instructionPointer + parameterPosition)]
        return self._memory[self._memory[(self._instructionPointer + parameterPosition)]]

    def Initialize(self, program, userInput):
        self._input = userInput
        #Don't surprise the user and change the incoming program
        self._Output = []
        self._memory = program.copy()
