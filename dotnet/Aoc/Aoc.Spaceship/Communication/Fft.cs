using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int[] GetPattern(int phaseStep)
        {
            //var newPattern = new List<int>();
            var newPattern = new int[Pattern.Length * phaseStep];
            var count = 0;
            for (var i = 1; i <= Pattern.Length; i++)
            {
                for(int j = 1; j <= phaseStep; j++)
                {
                    //newPattern.Add(Pattern[i]);
                    newPattern[count] = Pattern[i - 1];
                    count++;
                }
            }
            return newPattern;
        }

        public void ApplyPhases(int numberOfPhases)
        {
            for (var phase = 1; phase <= numberOfPhases; phase++)
                ApplyPhase(phase).Wait();
        }

        public async Task ApplyPhase(int phase)
        {
            var phaseData = new List<int>();
            var applyPhaseStepTasks = new List<Task<int>>();

            //Task.Run

            //Task.Run

            var range = Enumerable.Range(1, PhaseData[phase - 1].Count);
            var tasks = range.Select(i => ApplyPhaseStep(i, PhaseData[phase - 1]));
            var results = await Task.WhenAll(tasks);
            foreach (var result in results)
            {
                phaseData.Add(result);
            }

            //for(var i = 1; i <= PhaseData[phase - 1].Count; i++)
            //{
                    
            //    //var phaseInt = ApplyPhaseStep(i, PhaseData[phase - 1]);
            //    //applyPhaseStepTasks.Add(Task.Run((int) ApplyPhaseStep(i, PhaseData[phase - 1])));
            //    applyPhaseStepTasks.Add(ApplyPhaseStep(i, PhaseData[phase - 1]));
            //    //phaseData.Add(phaseInt);
            //}

            //Task.WaitAll(applyPhaseStepTasks.ToArray());
            //foreach (var taskInt in applyPhaseStepTasks)
            //{
            //    phaseData.Add(taskInt.Result);
            //}

            PhaseData.Add(phaseData);
        }

        public async Task<int> ApplyPhaseStep(int phaseStep, IList<int> initialPhaseData)
        {
            var patternPointer = 0;
            var sum = 0;
            var phaseDataCount = 0;

            for (var i = 0; i < initialPhaseData.Count; i++)
            {
                var phaseInput = initialPhaseData[i];
                sum += phaseInput * GetPatternInt(phaseStep, i);
                patternPointer++;
            }

            //var range = Enumerable.Range(0, initialPhaseData.Count - 1);
            //await Task.Run(() => Parallel.ForEach(range, i =>
            //{
            //    var phaseInput = initialPhaseData[i];
            //    sum += phaseInput * GetPatternInt(phaseStep, i);
            //    //patternPointer++;
            //}));


            //await Task.Run(() => Parallel.ForEach(initialPhaseData, phaseInput =>
            //{
            //    sum += phaseInput * GetPatternInt(phaseStep, patternPointer);
            //    patternPointer++;
            //}));

            //for (var i = 0; i < initialPhaseData.Count; i++)
            //    {
            //        var phaseInput = initialPhaseData[i];
            //        sum += phaseInput * GetPatternInt(phaseStep, patternPointer);
            //        patternPointer++;
            //    }



            //await Task.Run(() =>
            //{
            //    for (var i = 0; i < initialPhaseData.Count; i++)
            //    {
            //        var phaseInput = initialPhaseData[i];
            //        sum += phaseInput * GetPatternInt(phaseStep, patternPointer);
            //        patternPointer++;
            //    }
            //});
            return Math.Abs(sum % 10);
        }

        public int GetPatternInt(int phaseStep, int index)
        {
            var rIndex = ((index + 1) % (phaseStep * Pattern.Length)) / phaseStep;
            return Pattern[rIndex];
        }
    }
}
