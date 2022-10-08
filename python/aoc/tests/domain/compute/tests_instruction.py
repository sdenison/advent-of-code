import unittest

from domain.compute.instructions.compare import Compare
from domain.compute.instructions.instruction import Instruction
from domain.compute.instructions.instruction import ParameterMode
from domain.compute.instructions.jump import Jump
from domain.compute.instructions.math import Math
from domain.compute.opcodes import Opcodes


class TestsInstructions(unittest.TestCase):

    def test_create_instruction(self):
        instruction = Instruction(1)

        parameterModeEnum = instruction.GetParameterMode(100, 2)
        self.assertEqual(ParameterMode.Immediate, parameterModeEnum)
        parameterModeEnum = instruction.GetParameterMode(100, 3)
        self.assertEqual(ParameterMode.Reference, parameterModeEnum)


        parameterMode = ParameterMode(0)
        self.assertEqual(ParameterMode.Reference, parameterMode)

        opcode = Opcodes(3)

        match opcode:
            case Opcodes.Halt:
                self.assertTrue(True)
            case Opcodes.Put:
                self.assertTrue(True)
            case default:
                self.assertTrue(False)

    def test_jump_lambdas(self):
        isTrueLambda = lambda : True;
        instruction = Jump(2, isTrueLambda)
        self.assertTrue(instruction.ShouldJump(1))

    def test_math_lambdas(self):
        add = lambda x, y: x + y
        instruction = Math(1003, add)
        self.assertEqual(5, instruction.MathOperation(2, 3))

    def test_compare(self):
        less_than = lambda x, y: x < y
        instruction = Compare(1003, less_than)
        self.assertTrue(instruction.CompareFunction(0, 1))
        self.assertFalse(instruction.CompareFunction(0, 0))
        self.assertFalse(instruction.CompareFunction(1, 0))

    def test_something(self):
        self.assertEqual(True, True)


if __name__ == '__main__':
    unittest.main()

    def test_something(self):
        self.assertEqual(True, True)


if __name__ == '__main__':
    unittest.main()
