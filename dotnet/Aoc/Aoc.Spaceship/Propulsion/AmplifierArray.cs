using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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

        //public int GetMaximumThrustWithFeedbackLook(int numberOfAmplifiers)


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
