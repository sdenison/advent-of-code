using System.CommandLine;

namespace Aoc.Commands.Utilities;

internal class UtilitiesCommands : Command
{
    public UtilitiesCommands() : base("utilities", "Utilities command.")
    {
        Add(new DiagnosticsCommand());
    }
}