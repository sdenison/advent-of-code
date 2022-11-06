﻿using System;
using System.Collections.Concurrent;
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
            var newPattern = new int[Pattern.Length * phaseStep];
            var count = 0;
            for (var i = 1; i <= Pattern.Length; i++)
            {
                for(int j = 1; j <= phaseStep; j++)
                {
                    newPattern[count] = Pattern[i - 1];
                    count++;
                }
            }
            return newPattern;
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
            var phaseDataArray = new int[PhaseData[phase - 1].Count];
            Console.WriteLine($"Copy array start");
            var initialPhaseDataArray = PhaseData[phase - 1].ToArray();
            Console.WriteLine($"Copy array end");
            var dataSize = PhaseData[phase - 1].Count;
            int phaseStepProgress = 0;

            //This is the parallel for that works
            Parallel.For(0, dataSize / 2, (phaseStep) =>
            {
                phaseDataArray[phaseStep] = ApplyPhaseStep(phaseStep + 1, initialPhaseDataArray);// PhaseData[phase - 1]);
                if (phaseStep % 1000 == 0)
                {
                    Console.WriteLine($"Phase {phase}. Parallel phase step progress: {phaseStepProgress}. Done with phase step {phaseStep}.");
                    phaseStepProgress++;
                }
            });

            var total = 0;
            for (int phaseStep = PhaseData[phase - 1].Count - 1; phaseStep >= dataSize / 2; phaseStep--)
            {
                var newInt = ApplyPhaseStepHalf(phaseStep, initialPhaseDataArray);
                total += newInt;
                phaseDataArray[phaseStep] = total % 10;
                if (phaseStep % 1000 == 0)
                {
                    Console.WriteLine($"Phase {phase}. Last half phase step progress: {phaseStep}.");
                }
            }

            //for (int phaseStep = (dataSize / 2) - 1; phaseStep >= dataSize / 4; phaseStep--)
            //if (dataSize > 30)
            //{

        //    for (int phaseStep = (dataSize / 2) - 1; phaseStep * 3 > dataSize; phaseStep--)
        //    {
        //        var newInt = ApplyPhaseQuarter(phaseStep, initialPhaseDataArray, dataSize);
        //        total += newInt;
        //        phaseDataArray[phaseStep] = total % 10;
        //    }
        //}

        PhaseData.Add(phaseDataArray.ToList());
            
            return phaseDataArray.ToList().ToIntString();
        }

        public int ApplyPhaseQuarter(int phaseStep, int[] phaseData, int dataSize)
        {
            var backStep = dataSize - (dataSize / 2 - phaseStep);
            var amountToadd = phaseData[phaseStep];
            var amountToRemove = phaseData[backStep] + phaseData[backStep - 1];
            var amountToAdd = amountToadd - amountToRemove;
            return amountToAdd;
        }

        public int ApplyPhaseStepHalf(int phaseStep, int[] phaseData)
        {
            return phaseData[phaseStep];// * GetPatternInt(phaseStep, phaseStep);
        }


        public int ApplyPhaseStep(int phaseStep, int[] initialPhaseData) //IList<int> initialPhaseData)
        {
            var sum = 0;

            var phaseDataPointer = 0;
            int patternCounter = 0;
            if (phaseStep == 1)
            {
                patternCounter = 1;
            }
            var phaseDataSize = initialPhaseData.Length;
            while (phaseDataPointer < phaseDataSize)
            {
                int nextPhaseDataPointer;
                if (phaseDataPointer == 0)
                    if (phaseStep == 1)
                        nextPhaseDataPointer = 1;
                    else
                        nextPhaseDataPointer = phaseStep - 1;
                else 
                    nextPhaseDataPointer = Math.Min(phaseDataPointer + phaseStep, phaseDataSize);
                if (patternCounter == 0 || patternCounter == 2)
                {
                    phaseDataPointer = nextPhaseDataPointer;
                    patternCounter++;
                }
                else
                {
                    if (patternCounter == 1)
                    {
                        while (phaseDataPointer < nextPhaseDataPointer)
                        {
                            sum += initialPhaseData[phaseDataPointer];
                            phaseDataPointer++;
                        }
                        patternCounter++;
                    }
                    else
                    {
                        while (phaseDataPointer < nextPhaseDataPointer)
                        {
                            sum -= initialPhaseData[phaseDataPointer];
                            phaseDataPointer++;
                        }
                        patternCounter = 0;
                    }
                }
            }

            return Math.Abs(sum % 10);
        }
        public int ApplyPhaseStepOld(int phaseStep, IList<int> initialPhaseData)
        {
            var sum = 0;
            var patternInt = 0;
            for (var phaseDataPointer = 0; phaseDataPointer < initialPhaseData.Count; phaseDataPointer++)
            {
                if (0 == (phaseDataPointer + 1) % phaseStep)
                {
                    patternInt = GetPatternInt(phaseStep, phaseDataPointer);
                    if (patternInt == 0)
                    {
                        phaseDataPointer += phaseStep - 1;
                        continue;
                    }
                }
                sum += initialPhaseData[phaseDataPointer] * patternInt;
            }
            return Math.Abs(sum % 10);
        }

        public int GetPatternInt(int phaseStep, int index)
        {
            if (phaseStep == 0)
                return 1;
            var rIndex = ((index + 1) % (phaseStep * Pattern.Length)) / phaseStep;
            return Pattern[rIndex];
        }
    }
}
