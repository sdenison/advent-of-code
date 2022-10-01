using System.CommandLine;
using System.Threading.Tasks;
using Aoc.Commands;

namespace Aoc;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        var rootLevelCommands = new RootLevelCommands();
        await rootLevelCommands.InvokeAsync(args);
        return 0;
    }
}