using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Aoc.Spaceship.Propulsion
{
    public class AmplifierArray
    {
        private int[] _program;

        public AmplifierArray(int[] program)
        {
            _program = program;
        }

        public int GetThrust(int numberOfAmplifiers, int input, int[] phaseSettings)
        {
            var currentThrust = input;
            for (int i = 0; i < numberOfAmplifiers; i++)
            {
                var amplifier = new Amplifier(_program);
                currentThrust = amplifier.GetThrust(phaseSettings[i], currentThrust);
            }
            return currentThrust;
        }

        public int GetMaximumThrust(int numberOfAmplifiers, int[] initialPhaseSettings)
        {
            var maximumThrust = 0;
            var permutations = Permute(initialPhaseSettings);
            foreach (var phaseSettings in permutations)
            {
                var thrust = GetThrust(numberOfAmplifiers, 0, phaseSettings.ToArray());
                if (thrust > maximumThrust)
                    maximumThrust = thrust;
            }
            return maximumThrust;
        }

        public int GetMaximumThrustWithFeedbackLook(int[] initialPhaseSettings)
        {
            var amplifierA = new Amplifier(_program);
            var amplifierB = new Amplifier(_program);
            var amplifierC = new Amplifier(_program);
            var amplifierD = new Amplifier(_program);
            var amplifierE = new Amplifier(_program);
            int maximumThrust = 0;

            int amplifierAOutput = 0;
            amplifierA.HandleOutput = (output) =>
            {
                amplifierAOutput = output;
                if (output > maximumThrust)
                    maximumThrust = output;
            };
            amplifierB.AcceptInput = () => amplifierAOutput;

            int amplifierBOutput = 0;
            amplifierB.HandleOutput = (output) =>
            {
                amplifierBOutput = output;
                if (output > maximumThrust)
                    maximumThrust = output;
            };
            amplifierC.AcceptInput = () => amplifierBOutput;

            int amplifierCOutput = 0;
            amplifierC.HandleOutput = (output) =>
            {
                amplifierCOutput = output;
                if (output > maximumThrust)
                    maximumThrust = output;
            };
            amplifierD.AcceptInput = () => amplifierCOutput;

            int amplifierDOutput = 0;
            amplifierD.HandleOutput = (output) =>
            {
                amplifierDOutput = output;
                if (output > maximumThrust)
                    maximumThrust = output;
            };
            amplifierE.AcceptInput = () => amplifierDOutput;

            int amplifierEOutput = 0;
            amplifierE.HandleOutput = (output) =>
            {
                amplifierAOutput = output;
                if (output > maximumThrust)
                    maximumThrust = output;
            };
            amplifierA.AcceptInput = () => amplifierEOutput;

            
            var output = amplifierA.GetThrust(initialPhaseSettings[0], 0);
            output = amplifierB.GetThrust(initialPhaseSettings[1], 0);
            output = amplifierC.GetThrust(initialPhaseSettings[2], 0);
            output = amplifierD.GetThrust(initialPhaseSettings[3], 0);
            output = amplifierE.GetThrust(initialPhaseSettings[4], 0);


            return maximumThrust;
        }


        public IList<IList<int>> Permute(int[] nums)
        {
            var list = new List<IList<int>>();
            return DoPermute(nums, 0, nums.Length - 1, list);
        }

        static IList<IList<int>> DoPermute(int[] nums, int start, int end, IList<IList<int>> list)
        {
            if (start == end)
            {
                // We have one of our possible n! solutions,
                // add it to the list.
                list.Add(new List<int>(nums));
            }
            else
            {
                for (var i = start; i <= end; i++)
                {
                    Swap(ref nums[start], ref nums[i]);
                    DoPermute(nums, start + 1, end, list);
                    Swap(ref nums[start], ref nums[i]);
                }
            }

            return list;
        }

        static void Swap(ref int a, ref int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }
    }
}
