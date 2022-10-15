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
            var phaseSettingsPermutations = initialPhaseSettings.GetPermutations();
            foreach (var phaseSettings in phaseSettingsPermutations)
            {
                var thrust = GetThrust(phaseSettings.ToArray());
                if (thrust > maximumThrust)
                    maximumThrust = thrust;
            }
            return maximumThrust;
        }

        public int GetThrust(int[] phaseSettings)
        {
            //Creating the amplifiers
            var amplifierA = new Amplifier(_program, new[] {phaseSettings[0], 0});
            var amplifierB = new Amplifier(_program, phaseSettings[1]);
            var amplifierC = new Amplifier(_program, phaseSettings[2]);
            var amplifierD = new Amplifier(_program, phaseSettings[3]);
            var amplifierE = new Amplifier(_program, phaseSettings[4]);

            //Wiring the amplifiers together
            amplifierB.ConnectInputTo(amplifierA);
            amplifierC.ConnectInputTo(amplifierB);
            amplifierD.ConnectInputTo(amplifierC);
            amplifierE.ConnectInputTo(amplifierD);
            amplifierA.ConnectInputTo(amplifierE);

            //Starting the amplifier array
            var taskA = amplifierA.GetThrust();
            var taskB = amplifierB.GetThrust();
            var taskC = amplifierC.GetThrust();
            var taskD = amplifierD.GetThrust();
            var taskE = amplifierE.GetThrust();

            //Wait for the amplifiers to finish processing
            Task.WaitAll(taskA, taskB, taskC, taskD, taskE);

            //Return the thrust for amplifierE
            if (amplifierE.Thrust.HasValue)
                return amplifierE.Thrust.Value;

            throw new BrokenAmplifierArray("The amplifier array could not function with this configuration");
        }
    }
}
