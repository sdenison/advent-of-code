using System.Threading.Tasks;

namespace Aoc.Spaceship.Computer
{
    public interface IInputSource
    {
        Task<int> GetInput(int outputCounter);
    }
}
