using System.CommandLine;

namespace Aoc.Commands.Navigation
{
    public class NavigationCommands : Command
    {
        public NavigationCommands() : base("navigation", "Navigation commands")
        {
            Add(new CalculateMoonsCommand());
        }
    }
}
