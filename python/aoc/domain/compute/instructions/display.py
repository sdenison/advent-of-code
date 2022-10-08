from domain.compute.instructions.instruction import Instruction


class Display(Instruction):
    def __init__(self):
        super(Display, self).__init__(0)
    @property
    def Length(self):
        return 2