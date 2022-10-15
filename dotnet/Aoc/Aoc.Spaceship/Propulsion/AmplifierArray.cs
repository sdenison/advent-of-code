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
            amplifierB.ConnectInput(amplifierA);
            amplifierC.ConnectInput(amplifierB);
            amplifierD.ConnectInput(amplifierC);
            amplifierE.ConnectInput(amplifierD);
            amplifierA.ConnectInput(amplifierE);

            //Starting the amplifier array
            var taskA = amplifierA.GetThrust();
            var taskB = amplifierB.GetThrust();
            var taskC = amplifierC.GetThrust();
            var taskD = amplifierD.GetThrust();
            var taskE = amplifierE.GetThrust();

            //Wait for the amplifiers to finish processing
            Task.WaitAll(taskA, taskB, taskC, taskD, taskE);

            //Get the last output for amplifierE
            if (amplifierE.Thrust.HasValue)
                return amplifierE.Thrust.Value;

            throw new BrokenAmplifierArray("The amplifier array could not function with this configuration");
        }
    }
}
