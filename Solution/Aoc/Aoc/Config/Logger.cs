using System;
using Aoc.Domain.Infrastructure;

namespace Aoc.Config;

public class Logger : ILogger
{
    void ILogger.Error(string message)
    {
        Console.WriteLine($"ERROR: message");
    }

    void ILogger.Log(string message)
    {
        Console.WriteLine(message);
    }
}