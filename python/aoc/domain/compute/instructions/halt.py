from domain.compute.instructions.instruction import Instruction


class Halt(Instruction):
    def __init__(self):
        super(Halt, self).__init__(0)
    @property
    def Length(self):
        return 1