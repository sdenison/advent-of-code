using Aoc.Domain.Compute;
using NUnit.Framework;

namespace Aoc.Domain.Unit.Tests.Compute;

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
        var program = new[] {3, 0, 4, 0, 99};
        var expectedComputedOutput = new[] {3, 0, 4, 0, 99};
        var computedOutput = computer.RunProgram(program);

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
}