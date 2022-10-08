from domain.compute.instructions.instruction import Instruction


class Math(Instruction):
    @property
    def MathOperation(self):
        return self._MathOperation

    @property
    def Length(self):
        return 4

    def __init__(self, opcode, math_operation_param):
        super(Math, self).__init__(opcode)
        self._MathOperation = math_operation_param
