using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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
        var program = new[] {1, 2, 3, 4, 0};
        var expectedComputedOutput = new[] {1, 2, 3, 4, 5};
        var computedOutput = computer.RunProgram(program);

        Assert.AreEqual(expectedComputedOutput, computedOutput);
    }

    [Test]
    public void Can_process_multiply_opode()
    {
        var computer = new IntcodeComputer();
        var program = new[] {2, 2, 3, 4, 0};
        var expectedComputedOutput = new[] {1, 2, 3, 4, 6};
        var computedOutput = computer.RunProgram(program);

        Assert.AreEqual(expectedComputedOutput, computedOutput);
    }
}