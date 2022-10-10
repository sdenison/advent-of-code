namespace Aoc.Domain.Infrastructure
{
    public interface ILogger
    {
        void Log(string message);
        void Error(string message);
    }
}
