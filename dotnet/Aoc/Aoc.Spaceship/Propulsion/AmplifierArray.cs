using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Aoc.Spaceship.Propulsion
{
    public class AmplifierArray
    {
        private int[] _program;

        public AmplifierArray(int[] program)
        {
            _program = program;
        }

        public int GetMaximumThrust(int[] initialPhaseSettings)
        {
            var maximumThrust = 0;
            var permutations = Permute(initialPhaseSettings);
            foreach (var phaseSettings in permutations)
            {
                //var thrust = GetThrust(numberOfAmplifiers, 0, phaseSettings.ToArray());
                var thrust = GetMaximumThrustWithFeedbackLoop(phaseSettings.ToArray()).Result;
                if (thrust > maximumThrust)
                    maximumThrust = thrust;
            }
            return maximumThrust;
        }

        public async Task<int> GetMaximumThrustWithFeedbackLoop(int[] initialPhaseSettings)
        {
            //var amplifierA = new Amplifier(_program, new[] {initialPhaseSettings[0], 0});
            var amplifierA = new Amplifier(_program, new[] {initialPhaseSettings[0], 0});
            var amplifierB = new Amplifier(_program, initialPhaseSettings[1]);
            var amplifierC = new Amplifier(_program, initialPhaseSettings[2]);
            var amplifierD = new Amplifier(_program, initialPhaseSettings[3]);
            var amplifierE = new Amplifier(_program, initialPhaseSettings[4]);

            amplifierA.Computer.Name = "AAAAAAAAAAAAA";
            amplifierB.Computer.Name = "BBBBBBBBBBBBB";
            amplifierC.Computer.Name = "CCCCCCCCCCCCC";
            amplifierD.Computer.Name = "DDDDDDDDDDDDD";
            amplifierE.Computer.Name = "EEEEEEEEEEEEE";


            amplifierB.Computer.ParentComputer = amplifierA.Computer;
            amplifierC.Computer.ParentComputer = amplifierB.Computer;
            amplifierD.Computer.ParentComputer = amplifierC.Computer;
            amplifierE.Computer.ParentComputer = amplifierD.Computer;
            amplifierA.Computer.ParentComputer = amplifierE.Computer;

            //int amplifierAOutput = 0;
            //amplifierA.Computer.HandleOutput = amplifierB.Computer.AcceptInput2;
            //amplifierB.Computer.HandleOutput = amplifierC.Computer.AcceptInput2;
            //amplifierC.Computer.HandleOutput = amplifierD.Computer.AcceptInput2;
            //amplifierC.Computer.HandleOutput = amplifierD.Computer.AcceptInput2;
            //amplifierD.Computer.HandleOutput = amplifierE.Computer.AcceptInput2;
            //amplifierE.Computer.HandleOutput = amplifierA.Computer.AcceptInput2;


            var taskA = amplifierA.GetThrustAsync();
            var taskB = amplifierB.GetThrustAsync();
            var taskC = amplifierC.GetThrustAsync();
            var taskD = amplifierD.GetThrustAsync();
            var taskE = amplifierE.GetThrustAsync();

            Task.WaitAll(taskA, taskB, taskC, taskD, taskE);
            //Task.WaitAll(taskA);



            return amplifierE.Computer.Output[amplifierE.Computer.Output.Count - 1];
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
