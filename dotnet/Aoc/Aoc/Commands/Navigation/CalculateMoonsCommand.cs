using Aoc.Spaceship.Navigation.Moons;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc.Commands.Navigation
{
    internal class CalculateMoonsCommand : Command
    {
        public CalculateMoonsCommand() : base("calculatemoons", "Runs the moon calculation code")
        {
            this.SetHandler(() => 
            {
                CalculateMoons();
            });
        }

        public void CalculateMoons()
        {
            var moons = new List<string>
            {
                //Example with 4686774924 steps
                "<x=-8, y=-10, z=0>",
                "<x=5, y=5, z=10>",
                "<x=2, y=-7, z=3>",
                "<x=9, y=-8, z=-3>"

                //Puzzle input
                //"<x=0, y=6, z=1>",
                //"<x=4, y=4, z=19>",
                //"<x=-11, y=1, z=8>",
                //"<x=2, y=19, z=15>"
            };
            var planetarySystem = new PlanetarySystem(moons);
            Console.WriteLine($"Finding the repeated step...");
            var repeatStep = planetarySystem.FindRepeatingPattern();
            Console.WriteLine($"The first repeated step was {repeatStep}");
        }
    }
}
