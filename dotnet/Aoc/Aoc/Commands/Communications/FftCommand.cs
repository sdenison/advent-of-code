using Aoc.Spaceship.Communication;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aoc.Spaceship.Unit.Tests.CommunicationTests;
using Aoc.Spaceship.Utilities;
using System.Numerics;

namespace Aoc.Commands.Communications
{
    public class FftCommand : Command
    {
        public FftCommand() : base("fft", "Runs the fft command") 
        {
            this.SetHandler((writeToLog) =>
            {
                Fft();
            });
        }

        public void Fft_adf()
        {
            var input = FftTests.GetDay16PuzzleData();
            int[] pattern = {0, 1, 0, -1};
            var fft = new Fft(input, pattern);

            var path = "D:\\temp\\day16_step_1_console.txt";
            if (File.Exists(path))
                File.Delete(path);
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(input);

                foreach (var applyPhase in fft.ApplyPhases(100))
                {
                    sw.WriteLine(applyPhase);
                }
            }
        }

        public void Fft_fast()
        {
            var input = "03036732577212944063491565474664";
            var finalInput = "";
            for (var i = 0; i < 10000; i++)
                finalInput += input;
            int[] pattern = { 0, 1, 0, -1 };
            var fft = new Fft(finalInput, pattern);
            var path = "D:\\temp\\day_16_step_2_example_1_parallel_for.txt";
            if (File.Exists(path))
                File.Delete(path);
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(input);
            }
            foreach (var applyPhase in fft.ApplyPhases(100))
            {
                using StreamWriter sw = File.AppendText(path);
                sw.WriteLine(applyPhase);
            }
        }

        public void Fft()
        {
            var input = FftTests.GetDay16PuzzleData();
            var finalInput = "";
            for (var i = 0; i < 10000; i++)
                finalInput += input;
            int[] pattern = { 0, 1, 0, -1 };
            var fft = new Fft(finalInput, pattern);
            var path = "D:\\temp\\day_16_step_2_puzzle_data_1.txt";
            if (File.Exists(path))
                File.Delete(path);
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(input);
            }
            foreach (var applyPhase in fft.ApplyPhases(100))
            {
                using StreamWriter sw = File.AppendText(path);
                sw.WriteLine(applyPhase);
            }
        }
    }
}
