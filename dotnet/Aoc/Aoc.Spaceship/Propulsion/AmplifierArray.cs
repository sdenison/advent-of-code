using System;
using System.Collections.Generic;
using System.Text;

namespace Aoc.Spaceship.Propulsion
{
    public class AmplifierArray
    {
        private int[] _program;
        private int[] _phaseSetting;


        public AmplifierArray(int[] program, int[] phaseSetting)
        {
            _program = program;
            _phaseSetting = phaseSetting;
        }

        public int GetThrust(int numberOfAmplifiers, int input)
        {
            var currentThrust = input;
            for (int i = 0; i < numberOfAmplifiers; i++)
            {
                var amplifier = new Amplifier(_program);
                currentThrust = amplifier.GetThrust(_phaseSetting[i], currentThrust);
            }
            return currentThrust;
        }
    }
}
