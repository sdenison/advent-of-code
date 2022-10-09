using Aoc.Config;
using Aoc.Domain;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Aoc.Unit.Tests.Commands._2019.Day0;

[TestFixture]
public class Day0Tests
{

    [SetUp]
    public async Task SetUp()
    {
        DependencyInjection.RegisterLogger(new TestLogger());
        DependencyInjection.RegisteredServiceCollection = new ServiceCollection();
        DependencyInjection.RegisteredServiceCollection.AddSingleton<IAdder, Adder>();
    }

    [Test]
    public async Task Can_call_Add_command()
    {
        var args = new [] {
            "2019", "day0", "add", "--value-1", "5", "--value-2", "6"
        };
        var returnValue = await Program.Main(args);
        Assert.AreEqual(0, returnValue);
        var logger = (TestLogger)DependencyInjection.Logger;
        Assert.Contains("Adding 5 to 6 = 11", logger.LogEntries);
    }
}