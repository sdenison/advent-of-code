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
}