using System.CommandLine;

namespace Aoc.Commands._2019.Day0
{
    public class Day0Commands : Command
    {
        public Day0Commands() : base("day0", "Day 0 commands.")
        {
            Add(new AddCommand());
        }
    }
}
