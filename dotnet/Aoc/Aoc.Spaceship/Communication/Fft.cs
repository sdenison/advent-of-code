using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.Spaceship.Communication
{
    //Flawed Frequency Transmission algorithm
    public class Fft
    {
        public IList<IList<int>> PhaseData { get; }
        public int[] Pattern { get; }

        public Fft(string initialTransmission, int[] pattern) : this(initialTransmission.ToCharArray(), pattern)
        {
        }

        public Fft(char[] initialTransmission, int[] pattern) : this(initialTransmission.Select(x => int.Parse(x.ToString())).ToList(), pattern)
        {
        }

        public Fft(IList<int> initialTransmission, int[] pattern)
        {
            PhaseData = new List<IList<int>> {initialTransmission};
            Pattern = pattern;
        }

        public int[] GetPattern(int phase)
        {
            var newPattern = new List<int>();
            foreach (var patternItem in Pattern)
            {
                for(int j = 0; j < phase; j++)
                {
                    newPattern.Add(patternItem);
                }
            }
            return newPattern.ToArray();
        }

        public void ApplyPhases(int numberOfPhases)
        {
            for (var phase = 1; phase <= numberOfPhases; phase++)
                ApplyPhase(phase);
        }

        public void ApplyPhase(int phase)
        {
            var phaseData = new List<int>();
            for(var i = 1; i <= PhaseData[phase - 1].Count; i++)
            {
                if (i == 3)
                {
                    var x = "got here";
                }
                var phaseInt = ApplyPhaseStep(i, PhaseData[phase - 1]);
                phaseData.Add(phaseInt);
            }
            PhaseData.Add(phaseData);
        }

        public int ApplyPhaseStep(int phaseStep, IList<int> initialPhaseData)
        {
            var phasePattern = GetPattern(phaseStep);
            var output = new List<int>();
            var patternPointer = 0;
            //var offset = phaseStep == 1 ? 0 : 1;
            var offset = 1;
            foreach(var phaseInput in initialPhaseData)
            {
                output.Add(phaseInput * phasePattern[patternPointer + offset]);
                patternPointer++;
                if (patternPointer + offset == phasePattern.Length)
                {
                    patternPointer = 0;
                    offset = 0;
                }
            }
            return Math.Abs(output.Sum() % 10);
        }
    }
}
