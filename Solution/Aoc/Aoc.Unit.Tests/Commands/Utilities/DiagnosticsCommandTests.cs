﻿using Aoc.Config;
using NUnit.Framework;

namespace Aoc.Unit.Tests.Commands.Utilities;

[TestFixture]
public class DiagnosticsCommandTests
{
    [Test]
    public async Task Can_call_DisplayDiagnosticInformation()
    {
        DependencyInjection.RegisterLogger(new TestLogger());
        var args = new [] {
            "utilities", "diagnostics"
        };
        var returnValue = await Program.Main(args);
        Assert.AreEqual(0, returnValue);
    }
}