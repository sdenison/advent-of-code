using System.Linq;
using System.Threading.Tasks;
using Aoc.Spaceship.Utilities;

namespace Aoc.Spaceship.Propulsion
{
    public class AmplifierArray
    {
        private readonly int[] _program;

        public AmplifierArray(int[] program)
        {
            _program = program;
        }

        public int GetMaximumThrust(int[] initialPhaseSettings)
        {
            var maximumThrust = 0;
            var phaseSettingPermutations = initialPhaseSettings.GetPermutations();
            foreach (var phaseSettings in phaseSettingPermutations)
            {
                var thrust = GetThrust(phaseSettings.ToArray());
                if (thrust > maximumThrust)
                    maximumThrust = thrust;
            }
            return maximumThrust;
        }

        public int GetThrust(int[] initialPhaseSettings)
        {
            //Creating the amplifiers
            var amplifierA = new Amplifier(_program, new[] {initialPhaseSettings[0], 0});
            var amplifierB = new Amplifier(_program, initialPhaseSettings[1]);
            var amplifierC = new Amplifier(_program, initialPhaseSettings[2]);
            var amplifierD = new Amplifier(_program, initialPhaseSettings[3]);
            var amplifierE = new Amplifier(_program, initialPhaseSettings[4]);

            //Wiring the amplifiers together
            amplifierB.ConnectInput(amplifierA.Output);
            amplifierC.ConnectInput(amplifierB.Output);
            amplifierD.ConnectInput(amplifierC.Output);
            amplifierE.ConnectInput(amplifierD.Output);
            amplifierA.ConnectInput(amplifierE.Output);

            //Starting the amplifier array
            var taskA = amplifierA.GetThrust();
            var taskB = amplifierB.GetThrust();
            var taskC = amplifierC.GetThrust();
            var taskD = amplifierD.GetThrust();
            var taskE = amplifierE.GetThrust();

            //Wait for the amplifiers to finish processing
            Task.WaitAll(taskA, taskB, taskC, taskD, taskE);

            //Get the last output for amplifierE
            if (amplifierE.Computer.Output != null)
                return amplifierE.Computer.Output[^1];

            //If we get here then something went wrong
            throw new BrokenAmplifierArray("No output was produced for amplifier array");
        }
    }
}
