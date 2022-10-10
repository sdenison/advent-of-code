using System.CommandLine;
using Aoc.Commands._2019;
using Aoc.Commands.Utilities;

namespace Aoc.Commands;

public class RootLevelCommands : RootCommand
{
    public RootLevelCommands()
    {
        Add(new _2019Commands());
        Add(new UtilitiesCommands());
    }
}