using Aoc.Spaceship.Computer;

namespace Aoc.Spaceship.Propulsion
{
    public class Amplifier
    {
        private int[] _program;
        public Amplifier(int[] program)
        {
            _program = program;
        }

        public int GetThrust(int phaseSetting, int input)
        {
            var computer = new IntcodeComputer();
            var computerInput = new int[] {phaseSetting, input};
            computer.RunProgram(_program, computerInput);
            return computer.Output[0];
        }
    }
}
