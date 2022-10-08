from domain.compute.instructions.instruction import Instruction


class Compare(Instruction):
    @property
    def CompareFunction(self):
        return self._CompareFunction
    @property
    def Length(self):
        return 4
    def __init__(self, opcode, compareFunction):
        super(Compare, self).__init__(opcode)
        self._CompareFunction = compareFunction