from abc import ABC

from domain.compute.instructions.parameter_mode import ParameterMode

class Instruction(ABC):
    @property
    def Length(self):
        return self._Length
    @property
    def ParameterModes(self):
        return self._ParameterModes
    @ParameterModes.setter
    def ParameterModes(self, value):
        self._ParameterModes = value
    @staticmethod
    def GetParameterMode(opcode, positionIndex):
        return ParameterMode((opcode // 10**positionIndex) % 10)
    def __init__(self, opcode):
        self._Length = None
        self._ParameterModes = []
        self._ParameterModes.append(self.GetParameterMode(opcode, 2))
        self._ParameterModes.append(self.GetParameterMode(opcode, 3))
