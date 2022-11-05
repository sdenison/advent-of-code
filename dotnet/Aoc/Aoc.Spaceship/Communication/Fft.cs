using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Aoc.Spaceship.Utilities;

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

        public void ApplyPhaseMultiplier(int numberOfPhases, int multiplier)
        {
        }

        public IEnumerable<string> ApplyPhases(int numberOfPhases)
        {
            for (var phase = 1; phase <= numberOfPhases; phase++)
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                Console.WriteLine($"Starting phase {phase}");
                yield return ApplyPhase(phase);
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                Console.WriteLine($"Finished phase {phase} in {stopWatch.ElapsedMilliseconds / 1000} seconds");
            }
        }

        public string ApplyPhase(int phase)
        {
            var phaseData = new List<int>();
            var applyPhaseStepTasks = new List<Task<int>>();


            //var range = Enumerable.Range(1, PhaseData[phase - 1].Count);
            //var tasks = range.Select(i => ApplyPhaseStep(i, PhaseData[phase - 1]));
            //var results = await Task.WhenAll(tasks);
            //foreach (var result in results)
            //{
            //    phaseData.Add(result);
            //}

            


            var range = Enumerable.Range(0, PhaseData[phase - 1].Count - 1);




            var phaseDataArray = new int[PhaseData[phase - 1].Count];
            //Parallel.For(, range, i =>
            //{
            //    if (i == 0)
            //    {
            //        var x = i;
            //    }
            //    var phaseInt = ApplyPhaseStep(i + 1, PhaseData[phase - 1]);
            //    phaseDataArray[i] = phaseInt;
            //});

            Parallel.For(0, PhaseData[phase - 1].Count, (i) =>
            {
                //if (i + 1 % 10000 == 0)
                //    Console.WriteLine($"Processed phase step {i + 1} out of {PhaseData[phase - 1].Count}");
                var phaseInt = ApplyPhaseStep(i + 1, PhaseData[phase - 1]);
                //phaseData.Add(phaseInt);
                phaseDataArray[i] = phaseInt;
            });


            //for (var i = 1; i <= PhaseData[phase - 1].Count; i++)
            ////foreach(var i in range)
            //{
            //    if (i % 10000 == 0)
            //        Console.WriteLine($"Processed phase step {i} out of {PhaseData[phase - 1].Count}");
            //    var phaseInt = ApplyPhaseStep(i, PhaseData[phase - 1]);
            //    phaseData.Add(phaseInt);
            //}


            //PhaseData.Add(phaseData);
            PhaseData.Add(phaseDataArray.ToList());
            return phaseDataArray.ToList().ToIntString();
        }

        public async Task<int> ApplyPhaseStep2(int phaseStep, IList<int> initialPhaseData)
        {
            var sum = 0;

            for (var i = phaseStep - 1; i < initialPhaseData.Count; i++)
            {
                try
                {
                    var phaseInput = initialPhaseData[i];
                    sum += phaseInput * GetPatternInt(phaseStep, i);
                }
                catch (Exception ex)
                {
                    var x = ex.Message;
                }
            }

            return Math.Abs(sum % 10);
        }


        public int ApplyPhaseStep(int phaseStep, IList<int> initialPhaseData)
        {
            var sum = 0;
            var patternInt = 0;

            //for (var i = phaseStep - 1; i < initialPhaseData.Count; i++)
            for (var i = 0; i < initialPhaseData.Count; i++)
            {
                if (0 == (i + 1) % (phaseStep))
                {
                    patternInt = GetPatternInt(phaseStep, i);
                    if (patternInt == 0)
                    {
                        i += phaseStep - 1;
                        continue;
                    }
                }
                sum += initialPhaseData[i] * patternInt;
            }

            //var tasks = range.Select(i => initialPhaseData[i] * GetPatternInt(phaseStep, i));
            //var results = await Task.WaitAll(tasks);



            //var range = Enumerable.Range(0, initialPhaseData.Count);
            //await Task.Run(() => Parallel.ForEach(range, i =>
            //{
            //    var phaseInput = initialPhaseData[i];
            //    sum += phaseInput * GetPatternInt(phaseStep, i);
            //    //patternPointer++;
            //}));

            //Parallel.ForEach(range, (i) =>
            //{
            //    var phaseInput = initialPhaseData[i];
            //    sum += phaseInput * GetPatternInt(phaseStep, i);
            //});


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
