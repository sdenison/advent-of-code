using System;
using System.Threading.Tasks;
using Aoc.Spaceship.Computer;

namespace Aoc.Spaceship.Propulsion
{
    public class Amplifier
    {
        private int[] _program;
        public IntcodeComputer Computer { get; }
        public Func<int> AcceptInput { get; set; }
        public Action<int> HandleOutput { get; set; }
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

        public int GetThrust()
        {
            Computer.RunProgram(_program);
            return Computer.Output[Computer.Output.Count - 1];
        }

        public int GetThrust(int phaseSetting, int input)
        {
            var computer = Computer;
            var computerInput = new int[] {phaseSetting, input};
            //computer.AcceptInput = AcceptInput;
            //computer.HandleOutput = HandleOutput;
            computer.RunProgram(_program, computerInput);

            return computer.Output[computer.Output.Count - 1];
        }


        public int GetThrust(int phaseSetting)
        {
            var computer = Computer;
            var computerInput = new int[] {phaseSetting};
            //computer.AcceptInput = AcceptInput;
            //computer.HandleOutput = HandleOutput;
            computer.RunProgram(_program);

            return computer.Output[computer.Output.Count - 1];
        }

        public async Task GetThrustAsync()
        {
            var computer = Computer;
            //var computerInput = new int[] {phaseSetting};
            //computer.AcceptInput = AcceptInput;
            //computer.HandleOutput = HandleOutput;
            await computer.RunProgramAsync(_program, Input);

            //return computer.Output[computer.Output.Count - 1];
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
