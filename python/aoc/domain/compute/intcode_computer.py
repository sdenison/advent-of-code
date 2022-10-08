from domain.compute.instructions.compare import Compare
from domain.compute.instructions.display import Display
from domain.compute.instructions.halt import Halt
from domain.compute.instructions.instruction import Instruction
from domain.compute.instructions.jump import Jump
from domain.compute.instructions.math import Math
from domain.compute.instructions.parameter_mode import ParameterMode
from domain.compute.instructions.put import Put
from domain.compute.invalid_intcode_program import InvalidIntcodeProgram
from domain.compute.opcodes import Opcodes


class IntcodeComputer(object):
    def __init__(self):
        self._instruction_pointer: int = 0
        self._memory: list[int]
        self._input: int
        self._Output: list[int]

    @property
    def output(self):
        return self._Output

    def run_program(self, program: list[int], user_input) -> list[int]:
        self.__initialize(program, user_input)

        # Main computer logic
        while self._instruction_pointer < len(program):
            instruction = self.__get_next_instruction()
            if isinstance(instruction, Halt):
                return self._memory
            self._instruction_pointer = self.__execute_instruction(instruction)

        raise InvalidIntcodeProgram("No halt instruction at end of program")

    def __get_next_instruction(self) -> Instruction:
        opcode = self._memory[self._instruction_pointer]
        opcode_enum = Opcodes(opcode % 100)
        match opcode_enum:
            case Opcodes.HALT:
                return Halt()
            case Opcodes.DISPLAY:
                return Display()
            case Opcodes.PUT:
                return Put(opcode)
            case Opcodes.JUMPIFTRUE:
                return Jump(opcode, lambda: True)
            case Opcodes.JUMPIFFALSE:
                return Jump(opcode, lambda: False)
            case Opcodes.LESSTHAN:
                return Compare(opcode, lambda x, y: x < y)
            case Opcodes.EQUALS:
                return Compare(opcode, lambda x, y: x == y)
            case Opcodes.ADD:
                return Math(opcode, lambda x, y: x + y)
            case Opcodes.MULTIPLY:
                return Math(opcode, lambda x, y: x * y)

    def __execute_instruction(self, instruction: Instruction) -> int:
        if isinstance(instruction, Math):
            self.__execute_math_instruction(instruction)
        if isinstance(instruction, Display):
            self.__execute_display_instruction()
        if isinstance(instruction, Put):
            self.__execute_put_instruction(instruction)
        if isinstance(instruction, Compare):
            self.__execute_compare_instruction(instruction)
        if isinstance(instruction, Jump):
            return self.__execute_jump_instruction(instruction)
        return self._instruction_pointer + instruction.Length

    def __execute_jump_instruction(self, instruction: Jump) -> int:
        parameter1 = self.__get_parameter_value(instruction, 1)
        parameter2 = self.__get_parameter_value(instruction, 2)
        if instruction.ShouldJump(parameter1):
            return parameter2
        return self._instruction_pointer + instruction.Length

    def __execute_compare_instruction(self, instruction: Compare) -> None:
        parameter1 = self.__get_parameter_value(instruction, 1)
        parameter2 = self.__get_parameter_value(instruction, 2)
        destination_address = self._memory[(self._instruction_pointer + 3)]
        self._memory[destination_address] = (1 if instruction.CompareFunction(parameter1, parameter2) else 0)

    def __execute_math_instruction(self, instruction: Math) -> None:
        parameter1 = self.__get_parameter_value(instruction, 1)
        parameter2 = self.__get_parameter_value(instruction, 2)
        instruction_value = instruction.MathOperation(parameter1, parameter2)
        destination_address = self._memory[(self._instruction_pointer + 3)]
        self._memory[destination_address] = instruction_value

    def __execute_display_instruction(self) -> None:
        self._Output.append(self._memory[self._memory[(self._instruction_pointer + 1)]])

    def __execute_put_instruction(self, instruction: Put) -> None:
        if self._input is None:
            raise InvalidIntcodeProgram("This program expects input from user and none was given")
        if instruction.ParameterModes[0] == ParameterMode.IMMEDIATE:
            self._memory[(self._instruction_pointer + 1)] = self._input
        else:
            self._memory[self._memory[(self._instruction_pointer + 1)]] = self._input

    def __get_parameter_value(self, instruction: Instruction, parameter_position: int) -> int:
        if instruction.ParameterModes[(parameter_position - 1)] == ParameterMode.IMMEDIATE:
            return self._memory[(self._instruction_pointer + parameter_position)]
        return self._memory[self._memory[(self._instruction_pointer + parameter_position)]]

    def __initialize(self, program, user_input) -> None:
        self._input = user_input
        self._instruction_pointer = 0
        # Don't surprise the user and change the incoming program
        self._Output = []
        self._memory = program.copy()
