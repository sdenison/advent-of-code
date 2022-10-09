from abc import ABC
from enum import Enum


class Instruction(ABC):
    @property
    def length(self):
        return self._Length

    @property
    def parameter_modes(self):
        return self._ParameterModes

    @parameter_modes.setter
    def parameter_modes(self, value):
        self._ParameterModes = value

    @staticmethod
    def get_parameter_mode(opcode, position_index):
        return ParameterMode((opcode // 10 ** position_index) % 10)

    def __init__(self, opcode):
        self._Length = None
        self._ParameterModes = []
        self._ParameterModes.append(self.get_parameter_mode(opcode, 2))
        self._ParameterModes.append(self.get_parameter_mode(opcode, 3))


class Compare(Instruction):
    @property
    def compare_function(self):
        return self._compare_function

    @property
    def length(self):
        return 4

    def __init__(self, opcode, compare_function):
        super(Compare, self).__init__(opcode)
        self._compare_function = compare_function
        self._ParameterModes.append(self.get_parameter_mode(opcode, 3))


class Display(Instruction):
    def __init__(self):
        super(Display, self).__init__(0)

    @property
    def length(self):
        return 2


class Halt(Instruction):
    def __init__(self):
        super(Halt, self).__init__(0)

    @property
    def length(self):
        return 1


class Jump(Instruction):
    @property
    def _jumpIfTrue(self):
        return self.__jump_if_true

    @property
    def length(self):
        return 3

    def __init__(self, opcode, jump_if_true):
        super(Jump, self).__init__(opcode)
        self.__jump_if_true = jump_if_true

    def should_jump(self, parameter):
        if self.__jump_if_true():
            return parameter > 0
        return parameter == 0


class Math(Instruction):
    @property
    def math_operation(self):
        return self._math_operation

    @property
    def length(self):
        return 4

    def __init__(self, opcode, math_operation_param):
        super(Math, self).__init__(opcode)
        self._math_operation = math_operation_param
        self._ParameterModes.append(self.get_parameter_mode(opcode, 4))


class Put(Instruction):
    @property
    def length(self):
        return 2

    def __init__(self, opcode):
        super(Put, self).__init__(opcode)


class ParameterMode(Enum):
    REFERENCE = 0
    IMMEDIATE = 1


class Opcodes(Enum):
    ADD = 1
    MULTIPLY = 2
    PUT = 3
    DISPLAY = 4
    JUMPIFTRUE = 5
    JUMPIFFALSE = 6
    LESSTHAN = 7
    EQUALS = 8
    HALT = 99
