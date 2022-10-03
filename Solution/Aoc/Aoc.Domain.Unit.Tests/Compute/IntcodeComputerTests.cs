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
    public void Can_process_add_opode()
    {
        var computer = new IntcodeComputer();
        var program = new[] {1, 2, 3, 2, 99}; 
        var expectedComputedOutput = new[] {1, 2, 5, 2, 99};
        var computedOutput = computer.RunProgram(program);

        Assert.AreEqual(expectedComputedOutput, computedOutput);
    }

    [Test]
    public void Can_process_add_opode2()
    {
        var computer = new IntcodeComputer();
        var program = new[] {1, 2, 3, 2, 1, 5, 6, 6, 99}; 
        var expectedComputedOutput = new[] {1, 2, 5, 2, 1, 5, 11,6,99};
        var computedOutput = computer.RunProgram(program);

        Assert.AreEqual(expectedComputedOutput, computedOutput);
    }

    [Test]
    public void Can_process_multiply_opode()
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
    public void Can_process_example1_given_in_day1_problem(int[] program, int[] expectedComputedOutput)
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
        var x = computedOutput[0];
        Assert.AreEqual(4138687, computedOutput[0]);
    }
}