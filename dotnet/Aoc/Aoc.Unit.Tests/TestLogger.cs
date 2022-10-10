using Aoc.Domain.Infrastructure;

namespace Aoc.Unit.Tests
{
    public class TestLogger : ILogger
    {
        public List<string> LogEntries;

        public TestLogger()
        {
            LogEntries = new List<string>();
        }

        public void Error(string message)
        {
            LogEntries.Add($"ERROR: {message}");
        }

        public void Log(string message)
        {
            LogEntries.Add(message);
        }
    }
}
