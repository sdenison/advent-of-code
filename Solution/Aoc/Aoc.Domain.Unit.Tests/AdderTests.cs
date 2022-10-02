using NUnit.Framework;

namespace Aoc.Domain.Unit.Tests;

public class AdderTests
{
    [Test]
    public void Can_add_two_numbers()
    {
        var adder = new Adder();
        Assert.AreEqual(23, adder.Add(7, 16));
    }
}