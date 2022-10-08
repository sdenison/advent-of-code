from domain.compute.instructions.instruction import Instruction


class Jump(Instruction):
    @property
    def _jumpIfTrue(self):
        return self.__jumpIfTrue
    @property
    def Length(self):
        return 3
    def __init__(self, opcode, jumpIfTrue):
        super(Jump, self).__init__(opcode)
        self.__jumpIfTrue = jumpIfTrue
    def ShouldJump(self, parameter):
        if self.__jumpIfTrue():
            return (parameter > 0)
        return (parameter == 0)
