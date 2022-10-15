using System.Threading.Tasks;
using Aoc.Spaceship.Computer;

namespace Aoc.Spaceship.Propulsion
{
    public class Amplifier : IInputSource
    {
        private readonly int[] _program;
        private readonly int[] _input;
        private IntcodeComputer Computer { get; }
        internal int? Thrust => Computer.Output[^1];

        public Amplifier(int[] program, int input) : this(program, new [] {input})
        {
        }

        public Amplifier(int[] program, int[] input)
        {
            _program = program;
            Computer = new IntcodeComputer(input);
            _input = input;
        }

        public async Task GetThrust()
        {
            await Computer.RunProgramAsync(_program, _input);
        }

        public void ConnectInputTo(IInputSource inputSource)
        {
            Computer.InputPort = inputSource;
        }

        public async Task<int> GetInput(int outputCounter)
        {
            return await Computer.GetInput(outputCounter);
        }
    }
}
