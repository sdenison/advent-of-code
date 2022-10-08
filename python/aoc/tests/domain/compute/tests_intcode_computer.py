import unittest

from domain.compute.intcode_computer import IntcodeComputer

class TestsIntcodeUnit(unittest.TestCase):


    def test_can_create_intcode_computer(self):
        computer = IntcodeComputer()

    def test_that_add_works(self):
        computer = IntcodeComputer()
        program = [1, 2, 3, 2, 99]
        memoryOutput = computer.RunProgram(program, None)
        expectedMemoryOutput = [1, 2, 5, 2, 99];
        self.assertEqual(memoryOutput, expectedMemoryOutput)

    def test_can_process_multiply_opcode_test(self):
        computer = IntcodeComputer()
        program = [2, 2, 3, 2, 99]
        expectedComputedOutput = [2, 2, 6, 2, 99]
        computedOutput = computer.RunProgram(program, None)
        self.assertEqual(expectedComputedOutput, computedOutput)

    def test_can_process_examples_given_by_day1_problem_1(self):
        computer = IntcodeComputer()
        program = [1,0,0,0,99]
        expectedComputedOutput = [2,0,0,0,99]
        computedOutput = computer.RunProgram(program, None)
        self.assertEqual(expectedComputedOutput, computedOutput)

    def test_can_process_examples_given_by_day1_problem_2(self):
        computer = IntcodeComputer()
        program = [2,3,0,3,99]
        expectedComputedOutput = [2,3,0,6,99]
        computedOutput = computer.RunProgram(program, None)

    def test_can_process_examples_given_by_day1_problem(self):
        computer = IntcodeComputer()
        program = [2, 3, 0, 3, 99]
        expectedComputedOutput = [2, 3, 0, 6, 99]
        computedOutput = computer.RunProgram(program, None)
        self.assertEqual(expectedComputedOutput, computedOutput)

    #def get_day_1_examples(self):



    def test_something(self):
        self.assertEqual(True, True)

if __name__ == '__main__':
    unittest.main()
