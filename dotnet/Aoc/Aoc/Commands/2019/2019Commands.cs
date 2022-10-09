using System.CommandLine;
using Aoc.Commands._2019.Day0;

namespace Aoc.Commands._2019;

public class _2019Commands : Command
{
    public _2019Commands() : base("2019", "2019 commands.")
    {
        Add(new Day0Commands());
    }
}