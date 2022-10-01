using System.CommandLine;
using Aoc.Commands.Utilities;

namespace Aoc.Commands;

public class RootLevelCommands : RootCommand
{
    public RootLevelCommands()
    {
        Add(new UtilitiesCommands());
    }
}