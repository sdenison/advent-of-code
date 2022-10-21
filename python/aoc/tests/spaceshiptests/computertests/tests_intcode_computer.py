import unittest

from spaceship.computer.intcode_computer import *


class TestsIntcodeComputerUnit(unittest.TestCase):

    def test_that_we_can_create_intcode_computer(self):
        computer = IntcodeComputer()
        assert computer is not None

    def test_that_add_works(self):
        computer = IntcodeComputer()
        program = [1, 2, 3, 2, 99]
        memory_output = computer.run_program(program, None)
        expected_memory_output = [1, 2, 5, 2, 99]
        self.assertEqual(memory_output, expected_memory_output)

    def test_that_multiply_works(self):

        # run program on computer
        computer = IntcodeComputer()
        program = [2, 2, 3, 2, 99]
        computed_output = computer.run_program(program, None)


        expected_computed_output = [2, 2, 6, 2, 99]
        self.assertEqual(expected_computed_output, computed_output)

    @staticmethod
    def get_day_1_examples():
        return [[[99], [99]],
                [[99], [99]],
                [[2, 3, 0, 3, 99], [2, 3, 0, 6, 99]],
                [[2, 4, 4, 5, 99, 0], [2, 4, 4, 5, 99, 9801]],
                [[1, 1, 1, 4, 99, 5, 6, 0, 99], [30, 1, 1, 4, 2, 5, 6, 0, 99]]]

    def test_that_examples_given_by_day1_problem_work(self):
        computer = IntcodeComputer()
        datasets = self.get_day_1_examples()
        for dataset in datasets:
            computed_output = computer.run_program(dataset[0], None)
            self.assertEqual(dataset[1], computed_output)

    def test_get_answer_for_day_2_step_1(self):
        program = [1, 12, 2, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 6, 1, 19, 1, 19, 10, 23, 2, 13, 23, 27, 1, 5, 27,
                   31, 2, 6, 31, 35, 1, 6, 35, 39, 2, 39, 9, 43, 1, 5, 43, 47, 1, 13, 47, 51, 1, 10, 51, 55, 2, 55, 10,
                   59, 2, 10, 59, 63, 1, 9, 63, 67, 2, 67, 13, 71, 1, 71, 6, 75, 2, 6, 75, 79, 1, 5, 79, 83, 2, 83, 9,
                   87, 1, 6, 87, 91, 2, 91, 6, 95, 1, 95, 6, 99, 2, 99, 13, 103, 1, 6, 103, 107, 1, 2, 107, 111, 1, 111,
                   9, 0, 99, 2, 14, 0, 0]
        computer = IntcodeComputer()
        computed_output = computer.run_program(program, None)
        self.assertEqual(4138687, computed_output[0])

    def test_get_final_answer_for_day2_step_2(self):
        candidate_program = [1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 6, 1, 19, 1, 19, 10, 23, 2, 13, 23, 27,
                             1, 5, 27, 31, 2, 6, 31, 35, 1, 6, 35, 39, 2, 39, 9, 43, 1, 5, 43, 47, 1, 13, 47, 51, 1, 10,
                             51, 55, 2, 55, 10, 59, 2, 10, 59, 63, 1, 9, 63, 67, 2, 67, 13, 71, 1, 71, 6, 75, 2, 6, 75,
                             79, 1, 5, 79, 83, 2, 83, 9, 87, 1, 6, 87, 91, 2, 91, 6, 95, 1, 95, 6, 99, 2, 99, 13, 103,
                             1, 6, 103, 107, 1, 2, 107, 111, 1, 111, 9, 0, 99, 2, 14, 0, 0]
        computer = IntcodeComputer()
        solution_count = 0
        solution_noun = 0
        solution_verb = 0
        for noun in range(100):
            for verb in range(100):
                candidate_program[1] = noun
                candidate_program[2] = verb
                computed_output = computer.run_program(candidate_program, None)
                if computed_output[0] == 19690720:
                    solution_count = solution_count + 1
                    solution_noun = noun
                    solution_verb = verb
        # Curious if there was more than one solution
        self.assertEqual(1, solution_count)
        self.assertEqual(66, solution_noun)
        self.assertEqual(35, solution_verb)
        solution_value = (100 * solution_noun) + solution_verb
        self.assertEqual(6635, solution_value)

    def test_that_new_display_and_put_instructions_work(self):
        expected_computed_output = [55, 0, 4, 0, 99]

        # This is the program
        program = [3, 0, 4, 0, 99]

        # Create a computer
        computer = IntcodeComputer()

        # Run a program on the computer
        computer.run_program(program, 55)

        # Look at the output
        self.assertEqual(computer.output[0], 55)

    def test_that_parameter_modes_work(self):
        computer = IntcodeComputer()
        program = [1002, 4, 3, 4, 33]
        expected_computed_output = [1002, 4, 3, 4, 99]
        computed_output = computer.run_program(program, None)
        self.assertEqual(expected_computed_output, computed_output)

    def test_that_negative_numbers_are_valid(self):
        computer = IntcodeComputer()
        program = [1101, 100, -1, 4, 0]
        expected_computed_output = [1101, 100, -1, 4, 99]
        computed_output = computer.run_program(program, None)
        self.assertEqual(expected_computed_output, computed_output)

    def test_get_day5_step1_answer(self):
        computer = IntcodeComputer()
        program = self.get_day_2_data()
        computer.run_program(program, 1)
        output = computer.output
        self.assertEqual(13933662, output[9])

    def test_programs_that_return_true_if_input_is_equal_to_8(self):
        programs_to_test = [[3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8],
                            [3, 3, 1108, -1, 8, 3, 4, 3, 99]]
        for program in programs_to_test:
            computer = IntcodeComputer()
            computer.run_program(program, 8)
            self.assertEqual(1, computer.output[0])
            computer.run_program(program, 10)
            self.assertEqual(0, computer.output[0])
            computer.run_program(program, 5)
            self.assertEqual(0, computer.output[0])

    def test_programs_that_return_true_if_input_is_less_than_8(self):
        programs_to_test = [[3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8],
                            [3, 3, 1107, -1, 8, 3, 4, 3, 99]]
        for program in programs_to_test:
            computer = IntcodeComputer()
            computer.run_program(program, 8)
            self.assertEqual(0, computer.output[0])
            computer.run_program(program, 10)
            self.assertEqual(0, computer.output[0])
            computer.run_program(program, 5)
            self.assertEqual(1, computer.output[0])

    def test_get_day5_step2_answer(self):
        computer = IntcodeComputer()
        program = self.get_day_2_data()
        computer.run_program(program, 5)
        output = computer.output
        self.assertEqual(2369720, output[0])

    # Edge cases

    def test_that_99_is_a_valid_program(self):
        computer = IntcodeComputer()
        program = [99]
        expected_computed_output = [99]
        computed_output = computer.run_program(program, None)
        self.assertEqual(expected_computed_output, computed_output)

    def test_incomplete_instruction_should_raise_exception(self):
        program = [1, 2, 3, 2, 1, 3, 99]
        computer = IntcodeComputer()
        with self.assertRaises(InvalidIntcodeProgram):
            computer.run_program(program, None)

    def test_when_program_references_memory_that_does_not_belong_to_it_an_exception_is_raised(self):
        program = [4, 3, 99]
        computer = IntcodeComputer()
        with self.assertRaises(InvalidIntcodeProgram):
            computer.run_program(program, None)

    def test_that_invalid_opcode_should_raise_exception(self):
        program = [1, 2, 3, 3, 88]
        computer = IntcodeComputer()
        with self.assertRaises(InvalidIntcodeProgram):
            computer.run_program(program, None)

    def test_halt_code_needed_at_end_of_program(self):
        program = [1, 2, 3, 2]
        computer = IntcodeComputer()
        with self.assertRaises(InvalidIntcodeProgram):
            computer.run_program(program, None)

    def test_that_a_program_that_expects_input_and_doesnt_get_it_will_raise_exception(self):
        program = [3, 0, 4, 0, 99]
        computer = IntcodeComputer()
        with self.assertRaises(InvalidIntcodeProgram):
            computer.run_program(program, None)

    @classmethod
    def get_day_2_data(cls):
        return [3, 225, 1, 225, 6, 6, 1100, 1, 238, 225, 104, 0, 1101, 37, 61, 225, 101, 34, 121, 224, 1001, 224, -49,
                224, 4, 224, 102, 8, 223, 223, 1001, 224, 6, 224, 1, 224, 223, 223, 1101, 67, 29, 225, 1, 14, 65, 224,
                101, -124, 224, 224, 4, 224, 1002, 223, 8, 223, 101, 5, 224, 224, 1, 224, 223, 223, 1102, 63, 20, 225,
                1102, 27, 15, 225, 1102, 18, 79, 224, 101, -1422, 224, 224, 4, 224, 102, 8, 223, 223, 1001, 224, 1, 224,
                1, 223, 224, 223, 1102, 20, 44, 225, 1001, 69, 5, 224, 101, -32, 224, 224, 4, 224, 1002, 223, 8, 223,
                101, 1, 224, 224, 1, 223, 224, 223, 1102, 15, 10, 225, 1101, 6, 70, 225, 102, 86, 40, 224, 101, -2494,
                224, 224, 4, 224, 1002, 223, 8, 223, 101, 6, 224, 224, 1, 223, 224, 223, 1102, 25, 15, 225, 1101, 40,
                67, 224, 1001, 224, -107, 224, 4, 224, 102, 8, 223, 223, 101, 1, 224, 224, 1, 223, 224, 223, 2, 126, 95,
                224, 101, -1400, 224, 224, 4, 224, 1002, 223, 8, 223, 1001, 224, 3, 224, 1, 223, 224, 223, 1002, 151,
                84, 224, 101, -2100, 224, 224, 4, 224, 102, 8, 223, 223, 101, 6, 224, 224, 1, 224, 223, 223, 4, 223, 99,
                0, 0, 0, 677, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1105, 0, 99999, 1105, 227, 247, 1105, 1, 99999, 1005,
                227, 99999, 1005, 0, 256, 1105, 1, 99999, 1106, 227, 99999, 1106, 0, 265, 1105, 1, 99999, 1006, 0,
                99999, 1006, 227, 274, 1105, 1, 99999, 1105, 1, 280, 1105, 1, 99999, 1, 225, 225, 225, 1101, 294, 0, 0,
                105, 1, 0, 1105, 1, 99999, 1106, 0, 300, 1105, 1, 99999, 1, 225, 225, 225, 1101, 314, 0, 0, 106, 0, 0,
                1105, 1, 99999, 108, 677, 677, 224, 1002, 223, 2, 223, 1006, 224, 329, 101, 1, 223, 223, 1107, 677, 226,
                224, 102, 2, 223, 223, 1006, 224, 344, 101, 1, 223, 223, 8, 677, 677, 224, 1002, 223, 2, 223, 1006, 224,
                359, 101, 1, 223, 223, 1008, 677, 677, 224, 1002, 223, 2, 223, 1006, 224, 374, 101, 1, 223, 223, 7, 226,
                677, 224, 1002, 223, 2, 223, 1006, 224, 389, 1001, 223, 1, 223, 1007, 677, 677, 224, 1002, 223, 2, 223,
                1006, 224, 404, 1001, 223, 1, 223, 7, 677, 677, 224, 1002, 223, 2, 223, 1006, 224, 419, 1001, 223, 1,
                223, 1008, 677, 226, 224, 1002, 223, 2, 223, 1005, 224, 434, 1001, 223, 1, 223, 1107, 226, 677, 224,
                102, 2, 223, 223, 1005, 224, 449, 1001, 223, 1, 223, 1008, 226, 226, 224, 1002, 223, 2, 223, 1006, 224,
                464, 1001, 223, 1, 223, 1108, 677, 677, 224, 102, 2, 223, 223, 1006, 224, 479, 101, 1, 223, 223, 1108,
                226, 677, 224, 1002, 223, 2, 223, 1006, 224, 494, 1001, 223, 1, 223, 107, 226, 226, 224, 1002, 223, 2,
                223, 1006, 224, 509, 1001, 223, 1, 223, 8, 226, 677, 224, 102, 2, 223, 223, 1006, 224, 524, 1001, 223,
                1, 223, 1007, 226, 226, 224, 1002, 223, 2, 223, 1006, 224, 539, 1001, 223, 1, 223, 107, 677, 677, 224,
                1002, 223, 2, 223, 1006, 224, 554, 1001, 223, 1, 223, 1107, 226, 226, 224, 102, 2, 223, 223, 1005, 224,
                569, 101, 1, 223, 223, 1108, 677, 226, 224, 1002, 223, 2, 223, 1006, 224, 584, 1001, 223, 1, 223, 1007,
                677, 226, 224, 1002, 223, 2, 223, 1005, 224, 599, 101, 1, 223, 223, 107, 226, 677, 224, 102, 2, 223,
                223, 1005, 224, 614, 1001, 223, 1, 223, 108, 226, 226, 224, 1002, 223, 2, 223, 1005, 224, 629, 101, 1,
                223, 223, 7, 677, 226, 224, 102, 2, 223, 223, 1005, 224, 644, 101, 1, 223, 223, 8, 677, 226, 224, 102,
                2, 223, 223, 1006, 224, 659, 1001, 223, 1, 223, 108, 677, 226, 224, 102, 2, 223, 223, 1005, 224, 674,
                1001, 223, 1, 223, 4, 223, 99, 226]


if __name__ == '__main__':
    unittest.main()
