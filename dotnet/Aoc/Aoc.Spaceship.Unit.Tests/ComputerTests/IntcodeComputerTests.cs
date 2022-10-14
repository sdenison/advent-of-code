using Aoc.Spaceship.Computer;
using NUnit.Framework;

namespace Aoc.Spaceship.Unit.Tests.ComputerTests;

[TestFixture]
public class IntcodeComputerTests
{
    [Test]
    public void Can_create_IntCodeComputer()
    {
        var computer = new IntcodeComputer();
        Assert.IsNotNull(computer);
    }

    [Test]
    public void Can_process_add_opcode()
    {
        var computer = new IntcodeComputer();
        var program = new[] {1, 2, 3, 2, 99}; 
        var expectedComputedOutput = new[] {1, 2, 5, 2, 99};
        var computedOutput = computer.RunProgram(program);

        Assert.AreEqual(expectedComputedOutput, computedOutput);
    }

    [Test]
    public void Can_process_add_opcode2()
    {
        var computer = new IntcodeComputer();
        var program = new[] {1, 2, 3, 2, 1, 5, 6, 6, 99}; 
        var expectedComputedOutput = new[] {1, 2, 5, 2, 1, 5, 11, 6, 99};
        var computedOutput = computer.RunProgram(program);

        Assert.AreEqual(expectedComputedOutput, computedOutput);
    }

    [Test]
    public void Can_process_multiply_opcode()
    {
        var computer = new IntcodeComputer();
        var program = new[] {2, 2, 3, 2, 99};
        var expectedComputedOutput = new[] {2, 2, 6, 2, 99};
        var computedOutput = computer.RunProgram(program);

        Assert.AreEqual(expectedComputedOutput, computedOutput);
    }

    [Test]
    [TestCase(new[]{1,0,0,0,99}, new[] {2,0,0,0,99})]
    [TestCase(new[]{2,3,0,3,99}, new[] {2,3,0,6,99})]
    [TestCase(new[]{2,4,4,5,99,0}, new[] {2,4,4,5,99,9801})]
    [TestCase(new[]{1,1,1,4,99,5,6,0,99}, new[] {30,1,1,4,2,5,6,0,99})]
    public void Can_process_examples_given_in_day1_problem(int[] program, int[] expectedComputedOutput)
    {
        var computer = new IntcodeComputer();
        var computedOutput = computer.RunProgram(program);

        Assert.AreEqual(expectedComputedOutput, computedOutput);
    }

    [Test]
    public void Can_get_final_answer_for_day2_step1()
    {
        var program = new[]
        {
            1, 12, 2, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 6, 1, 19, 1, 19, 10, 23, 2, 13, 23, 27, 1, 5, 27, 31, 2,
            6, 31, 35, 1, 6, 35, 39, 2, 39, 9, 43, 1, 5, 43, 47, 1, 13, 47, 51, 1, 10, 51, 55, 2, 55, 10, 59, 2, 10, 59,
            63, 1, 9, 63, 67, 2, 67, 13, 71, 1, 71, 6, 75, 2, 6, 75, 79, 1, 5, 79, 83, 2, 83, 9, 87, 1, 6, 87, 91, 2,
            91, 6, 95, 1, 95, 6, 99, 2, 99, 13, 103, 1, 6, 103, 107, 1, 2, 107, 111, 1, 111, 9, 0, 99, 2, 14, 0, 0
        };
        var computer = new IntcodeComputer();
        var computedOutput = computer.RunProgram(program);

        Assert.AreEqual(4138687, computedOutput[0]);
    }

    [Test]
    public void Can_get_final_answer_for_day2_step2()
    {
        var candidateProgram = new[]
        {
            1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 6, 1, 19, 1, 19, 10, 23, 2, 13, 23, 27, 1, 5, 27, 31, 2,
            6, 31, 35, 1, 6, 35, 39, 2, 39, 9, 43, 1, 5, 43, 47, 1, 13, 47, 51, 1, 10, 51, 55, 2, 55, 10, 59, 2, 10, 59,
            63, 1, 9, 63, 67, 2, 67, 13, 71, 1, 71, 6, 75, 2, 6, 75, 79, 1, 5, 79, 83, 2, 83, 9, 87, 1, 6, 87, 91, 2,
            91, 6, 95, 1, 95, 6, 99, 2, 99, 13, 103, 1, 6, 103, 107, 1, 2, 107, 111, 1, 111, 9, 0, 99, 2, 14, 0, 0
        };
        var computer = new IntcodeComputer();
        var foundValue = false;
        var solutions = 0;
        var solutionNoun = 0;
        var solutionVerb = 0;
        foreach (var noun in Enumerable.Range(0, 100))
        {
            foreach (var verb in Enumerable.Range(0, 100))
            {
                candidateProgram[1] = noun;
                candidateProgram[2] = verb;
                var computedOutput = computer.RunProgram(candidateProgram);
                if (computedOutput[0] == 19690720)
                {
                    foundValue = true;
                    solutions++;
                    solutionNoun = noun;
                    solutionVerb = verb;
                }
            }
        }
        Assert.IsTrue(foundValue);
        Assert.AreEqual(1, solutions);
        Assert.AreEqual(66, solutionNoun);
        Assert.AreEqual(35, solutionVerb);
        var solutionValue = (100 * solutionNoun) + solutionVerb;
        Assert.AreEqual(6635, solutionValue);
    }

    [Test]
    public void Can_use_new_Display_and_Put_instructions()
    {
        var computer = new IntcodeComputer();
        var program = new[] { 3, 0, 4, 0, 99 };
        var expectedComputedOutput = new[] { 55, 0, 4, 0, 99 };
        var computedOutput = computer.RunProgram(program, 55);
        var output = computer.Output;

        Assert.AreEqual(expectedComputedOutput, computedOutput);
    }

    [Test]
    public void Can_use_parameter_modes()
    {
        var computer = new IntcodeComputer();
        var program = new[] {1002, 4, 3, 4, 33};
        var expectedComputedOutput = new[] {1002, 4, 3, 4, 99};
        var computedOutput = computer.RunProgram(program);

        Assert.AreEqual(expectedComputedOutput, computedOutput);
    }

    [Test]
    public void Can_use_negative_numbers()
    {
        var computer = new IntcodeComputer();
        var program = new[] {1101, 100, -1, 4, 0};
        var expectedComputedOutput = new[] {1101, 100, -1, 4, 99};
        var computedOutput = computer.RunProgram(program);

        Assert.AreEqual(expectedComputedOutput, computedOutput);
    }

    [Test]
    public void Get_day5_step1_answer()
    {
        var computer = new IntcodeComputer();
        var program = GetDay2Data();
        var computedOutput = computer.RunProgram(program, 1);
        var output = computer.Output;

        Assert.AreEqual(13933662, output[9]);
    }

    [Test]
    [TestCase(new int[] { 3,9,8,9,10,9,4,9,99,-1,8 })]
    [TestCase(new int[] { 3,3,1108,-1,8,3,4,3,99 })]
    public void Can_pass_jump_test_is_input_equal_to_8(int[] program)
    {
        //var program = new[] {3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8};
        var computer = new IntcodeComputer();
        //Input is 8 so output should be 0
        var computedOutput = computer.RunProgram(program, 8);
        Assert.AreEqual(1, computer.Output[0]);
        //Input is greater than 8 so output should be 0
        computedOutput = computer.RunProgram(program, 10);
        Assert.AreEqual(0, computer.Output[0]);
        //Input is less than 8 so output should be 0
        computedOutput = computer.RunProgram(program, 5);
        Assert.AreEqual(0, computer.Output[0]);
    }

    [Test]
    [TestCase(new int[] { 3,9,7,9,10,9,4,9,99,-1,8 })]
    [TestCase(new int[] { 3,3,1107,-1,8,3,4,3,99 })]
    public void Can_pass_jump_test_input_is_less_than_8(int[] program)
    {
        var computer = new IntcodeComputer();
        //Input is 8 so output should be 0
        var computedOutput = computer.RunProgram(program, 8);
        Assert.AreEqual(0, computer.Output[0]);
        //Input is greater than 8 so output should be 0
        computedOutput = computer.RunProgram(program, 10);
        Assert.AreEqual(0, computer.Output[0]);
        //Input is less than 8 so output should be 1
        computedOutput = computer.RunProgram(program, 6);
        Assert.AreEqual(1, computer.Output[0]);
    }

    [Test]
    public void Get_day5_step2_answer()
    {
        var computer = new IntcodeComputer();
        var program = GetDay2Data();
        var computedOutput = computer.RunProgram(program, 5);
        var output = computer.Output;

        Assert.AreEqual(2369720, output[0]);
    }

    [Test]
    public void Can_prompt_for_input()
    {
        var computer = new IntcodeComputer();
        var program = new[] { 3, 0, 4, 0, 99 };
        var expectedComputedOutput = new[] { 55, 0, 4, 0, 99 };
        computer.AcceptInput = () => 55;
        var computedOutput = computer.RunProgram(program);
        var output = computer.Output;
        Assert.AreEqual(expectedComputedOutput, computedOutput);
    }

    [Test]
    public void Can_handle_output()
    {
        var computer = new IntcodeComputer();
        var program = new[] { 3, 0, 4, 0, 99 };
        var expectedComputedOutput = new[] { 55, 0, 4, 0, 99 };
        computer.AcceptInput = () => 55;
        computer.HandleOutput = HandlingOutput;
        var computedOutput = computer.RunProgram(program);
        var output = computer.Output;
        Assert.AreEqual(expectedComputedOutput, computedOutput);
    }

    public void HandlingOutput(int output)
    {
        var x = output;
    }

    //Edge cases

    [Test]
    public void _99_is_a_valid_program()
    {
        var computer = new IntcodeComputer();
        var program = new[] {99}; 
        var expectedComputedOutput = new[] {99};
        var computedOutput = computer.RunProgram(program);

        Assert.AreEqual(expectedComputedOutput, computedOutput);
    }

    [Test]
    public void Incomplete_instruction_should_throw_exception()
    {
        var program = new[] {1, 2, 3, 2, 1, 3, 99}; 
        var computer = new IntcodeComputer();
        Assert.Throws<InvalidIntcodeProgram>(() =>
        {
            var computedOutput = computer.RunProgram(program);
        });
    }

    [Test]
    public void Invalid_opcode_throws_exception()
    {
        var program = new[] {1, 2, 3, 3, 88}; 
        var computer = new IntcodeComputer();
        Assert.Throws<InvalidIntcodeProgram>(() =>
        {
            var computedOutput = computer.RunProgram(program);
        });
    }

    [Test]
    public void Halt_code_needed_at_end()
    {
        var program = new[] {1, 2, 3, 2}; 
        var computer = new IntcodeComputer();
        Assert.Throws<InvalidIntcodeProgram>(() =>
        {
            var computedOutput = computer.RunProgram(program);
        });
    }

    //[Test]
    //public void Running_a_program_that_expects_input_but_none_was_given_should_fail()
    //{
    //    var program = new[] { 3, 0, 4, 0, 99 };
    //    var computer = new IntcodeComputer();
    //    Assert.Throws<InvalidIntcodeProgram>(() =>
    //    {
    //        var computedOutput = computer.RunProgram(program);
    //    });
    //}

    //Data generation

    private int[] GetDay2Data()
    {
        return new[]
        {
            3, 225, 1, 225, 6, 6, 1100, 1, 238, 225, 104, 0, 1101, 37, 61, 225, 101, 34, 121, 224, 1001, 224, -49, 224,
            4, 224, 102, 8, 223, 223, 1001, 224, 6, 224, 1, 224, 223, 223, 1101, 67, 29, 225, 1, 14, 65, 224, 101, -124,
            224, 224, 4, 224, 1002, 223, 8, 223, 101, 5, 224, 224, 1, 224, 223, 223, 1102, 63, 20, 225, 1102, 27, 15,
            225, 1102, 18, 79, 224, 101, -1422, 224, 224, 4, 224, 102, 8, 223, 223, 1001, 224, 1, 224, 1, 223, 224, 223,
            1102, 20, 44, 225, 1001, 69, 5, 224, 101, -32, 224, 224, 4, 224, 1002, 223, 8, 223, 101, 1, 224, 224, 1,
            223, 224, 223, 1102, 15, 10, 225, 1101, 6, 70, 225, 102, 86, 40, 224, 101, -2494, 224, 224, 4, 224, 1002,
            223, 8, 223, 101, 6, 224, 224, 1, 223, 224, 223, 1102, 25, 15, 225, 1101, 40, 67, 224, 1001, 224, -107, 224,
            4, 224, 102, 8, 223, 223, 101, 1, 224, 224, 1, 223, 224, 223, 2, 126, 95, 224, 101, -1400, 224, 224, 4, 224,
            1002, 223, 8, 223, 1001, 224, 3, 224, 1, 223, 224, 223, 1002, 151, 84, 224, 101, -2100, 224, 224, 4, 224,
            102, 8, 223, 223, 101, 6, 224, 224, 1, 224, 223, 223, 4, 223, 99, 0, 0, 0, 677, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 1105, 0, 99999, 1105, 227, 247, 1105, 1, 99999, 1005, 227, 99999, 1005, 0, 256, 1105, 1, 99999, 1106,
            227, 99999, 1106, 0, 265, 1105, 1, 99999, 1006, 0, 99999, 1006, 227, 274, 1105, 1, 99999, 1105, 1, 280,
            1105, 1, 99999, 1, 225, 225, 225, 1101, 294, 0, 0, 105, 1, 0, 1105, 1, 99999, 1106, 0, 300, 1105, 1, 99999,
            1, 225, 225, 225, 1101, 314, 0, 0, 106, 0, 0, 1105, 1, 99999, 108, 677, 677, 224, 1002, 223, 2, 223, 1006,
            224, 329, 101, 1, 223, 223, 1107, 677, 226, 224, 102, 2, 223, 223, 1006, 224, 344, 101, 1, 223, 223, 8, 677,
            677, 224, 1002, 223, 2, 223, 1006, 224, 359, 101, 1, 223, 223, 1008, 677, 677, 224, 1002, 223, 2, 223, 1006,
            224, 374, 101, 1, 223, 223, 7, 226, 677, 224, 1002, 223, 2, 223, 1006, 224, 389, 1001, 223, 1, 223, 1007,
            677, 677, 224, 1002, 223, 2, 223, 1006, 224, 404, 1001, 223, 1, 223, 7, 677, 677, 224, 1002, 223, 2, 223,
            1006, 224, 419, 1001, 223, 1, 223, 1008, 677, 226, 224, 1002, 223, 2, 223, 1005, 224, 434, 1001, 223, 1,
            223, 1107, 226, 677, 224, 102, 2, 223, 223, 1005, 224, 449, 1001, 223, 1, 223, 1008, 226, 226, 224, 1002,
            223, 2, 223, 1006, 224, 464, 1001, 223, 1, 223, 1108, 677, 677, 224, 102, 2, 223, 223, 1006, 224, 479, 101,
            1, 223, 223, 1108, 226, 677, 224, 1002, 223, 2, 223, 1006, 224, 494, 1001, 223, 1, 223, 107, 226, 226, 224,
            1002, 223, 2, 223, 1006, 224, 509, 1001, 223, 1, 223, 8, 226, 677, 224, 102, 2, 223, 223, 1006, 224, 524,
            1001, 223, 1, 223, 1007, 226, 226, 224, 1002, 223, 2, 223, 1006, 224, 539, 1001, 223, 1, 223, 107, 677, 677,
            224, 1002, 223, 2, 223, 1006, 224, 554, 1001, 223, 1, 223, 1107, 226, 226, 224, 102, 2, 223, 223, 1005, 224,
            569, 101, 1, 223, 223, 1108, 677, 226, 224, 1002, 223, 2, 223, 1006, 224, 584, 1001, 223, 1, 223, 1007, 677,
            226, 224, 1002, 223, 2, 223, 1005, 224, 599, 101, 1, 223, 223, 107, 226, 677, 224, 102, 2, 223, 223, 1005,
            224, 614, 1001, 223, 1, 223, 108, 226, 226, 224, 1002, 223, 2, 223, 1005, 224, 629, 101, 1, 223, 223, 7,
            677, 226, 224, 102, 2, 223, 223, 1005, 224, 644, 101, 1, 223, 223, 8, 677, 226, 224, 102, 2, 223, 223, 1006,
            224, 659, 1001, 223, 1, 223, 108, 677, 226, 224, 102, 2, 223, 223, 1005, 224, 674, 1001, 223, 1, 223, 4,
            223, 99, 226
        };
    }
}