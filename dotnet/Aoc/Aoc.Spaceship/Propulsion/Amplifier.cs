using System;
using Aoc.Spaceship.Computer;

namespace Aoc.Spaceship.Propulsion
{
    public class Amplifier
    {
        private int[] _program;
        private IntcodeComputer _computer;
        public Func<int> AcceptInput { get; set; }
        public Action<int> HandleOutput { get; set; }


        public Amplifier(int[] program)
        {
            _program = program;
            _computer = new IntcodeComputer();
        }


        public int GetThrust(int phaseSetting, int input)
        {
            var computer = new IntcodeComputer();
            var computerInput = new int[] {phaseSetting, input};
            computer.AcceptInput = AcceptInput;
            computer.HandleOutput = HandleOutput;
            computer.RunProgram(_program, computerInput);

            return computer.Output[0];
        }



        //public int AcceptInput( )
        //{
        //    _computer.AcceptInput = 
        //}

        //public int HandleOutput(int output)
        //{

        //}
    }
}
