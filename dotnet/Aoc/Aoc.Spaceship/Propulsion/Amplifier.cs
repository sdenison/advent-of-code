using System.Threading.Tasks;
using Aoc.Spaceship.Computer;

namespace Aoc.Spaceship.Propulsion
{
    public class Amplifier
    {
        private int[] _program;
        public IntcodeComputer Computer { get; }
        public IInputSource Output => Computer;
        public int[] Input { get; set; }

        public Amplifier(int[] program, int input) : this(program, new [] {input})
        {
        }

        public Amplifier(int[] program, int[] input)
        {
            _program = program;
            Computer = new IntcodeComputer(input);
            Input = input;
        }

        public async Task GetThrust()
        {
            await Computer.RunProgramAsync(_program, Input);
        }

        public void ConnectInput(IInputSource inputSource)
        {
            Computer.InputPort = inputSource;
        }
    }
}
