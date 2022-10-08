from domain.compute.instructions.instruction import Instruction


class Put(Instruction):
    @property
    def Length(self):
        return 2
    def __init__(self, opcode):
        super(Put, self).__init__(opcode)