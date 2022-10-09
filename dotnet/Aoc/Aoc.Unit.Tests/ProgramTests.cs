using NUnit.Framework;

namespace Aoc.Unit.Tests;

[TestFixture]
public class ProgramTests
{
    [Test]
    public async Task Can_create_Program_object()
    {
        var returnValue = await Program.Main(new string[] {"fake", "command"});
        //Random commands return a code of 1
        Assert.AreEqual(1, returnValue);
    }
}