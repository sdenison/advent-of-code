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

    def get_day_1_examples(self):
        return [[[99], [99]],
                [[99], [99]],
                [[2, 3, 0, 3, 99], [2, 3, 0, 6, 99]],
                [[2, 4, 4, 5, 99, 0], [2, 4, 4, 5, 99, 9801]],
                [[1, 1, 1, 4, 99, 5, 6, 0, 99], [30, 1, 1, 4, 2, 5, 6, 0, 99]]]

    def test_can_process_examples_given_by_day1_problem(self):
        datasets = self.get_day_1_examples()
        for dataset in datasets:
            computer = IntcodeComputer()
            computedoutput = computer.RunProgram(dataset[0], None)
            self.assertEqual(dataset[1], computedoutput)

    def test_can_get_final_answer_for_day2_step_1(self):
        program = [1, 12, 2, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 6, 1, 19, 1, 19, 10, 23, 2, 13, 23, 27, 1, 5, 27, 31, 2,
            6, 31, 35, 1, 6, 35, 39, 2, 39, 9, 43, 1, 5, 43, 47, 1, 13, 47, 51, 1, 10, 51, 55, 2, 55, 10, 59, 2, 10, 59,
            63, 1, 9, 63, 67, 2, 67, 13, 71, 1, 71, 6, 75, 2, 6, 75, 79, 1, 5, 79, 83, 2, 83, 9, 87, 1, 6, 87, 91, 2,
            91, 6, 95, 1, 95, 6, 99, 2, 99, 13, 103, 1, 6, 103, 107, 1, 2, 107, 111, 1, 111, 9, 0, 99, 2, 14, 0, 0]
        computer = IntcodeComputer()
        computedoutput = computer.RunProgram(program, None)
        self.assertEqual(4138687, computedoutput[0])

    def test_can_get_final_answer_for_day2_step_2(self):
        candidateProgram = [1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 6, 1, 19, 1, 19, 10, 23, 2, 13, 23, 27, 1, 5, 27, 31, 2,
            6, 31, 35, 1, 6, 35, 39, 2, 39, 9, 43, 1, 5, 43, 47, 1, 13, 47, 51, 1, 10, 51, 55, 2, 55, 10, 59, 2, 10, 59,
            63, 1, 9, 63, 67, 2, 67, 13, 71, 1, 71, 6, 75, 2, 6, 75, 79, 1, 5, 79, 83, 2, 83, 9, 87, 1, 6, 87, 91, 2,
            91, 6, 95, 1, 95, 6, 99, 2, 99, 13, 103, 1, 6, 103, 107, 1, 2, 107, 111, 1, 111, 9, 0, 99, 2, 14, 0, 0]
        computer = IntcodeComputer()
        solutioncount = 0
        for noun in range(100):
            for verb in range(100):
                candidateProgram[1] = noun
                candidateProgram[2] = verb
                computedOutput = computer.RunProgram(candidateProgram, None)
                if (computedOutput[0] == 19690720):
                    solutioncount = solutioncount + 1
                    solutionNoun = noun
                    solutionVerb = verb
        #Curious if there was more than one solution
        self.assertEqual(1, solutioncount)
        self.assertEqual(66, solutionNoun)
        self.assertEqual(35, solutionVerb)
        solutionValue = (100 * solutionNoun) + solutionVerb
        self.assertEqual(6635, solutionValue)

    def test_something(self):
        self.assertEqual(True, True)


if __name__ == '__main__':
    unittest.main()
