using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Aoc.Spaceship.Navigation.Moons
{
    public class PlanetarySystem
    {
        public IList<Moon> Moons { get; set; }
        public IList<Moon> InitialMoonConfiguration { get; set; }
        public SortedDictionary<string, int> EnergyList { get; set; }


        public int TotalEnergy
        {
            get { return Moons.Select(x => x.TotalEnergy).Sum(); }
        }

        public PlanetarySystem(IList<string> moonCoordinates)
        {
            EnergyList = new SortedDictionary<string, int>();
            Moons = new List<Moon>();
            InitialMoonConfiguration = new List<Moon>();
            foreach(var moon in moonCoordinates)
            {
                Moons.Add(new Moon(moon));
                InitialMoonConfiguration.Add(new Moon(moon));
            }
            EnergyList.Add(ToString(), 0);
        }

        public void TakeTimeSteps(int stepsToTake)
        {
            for (int i = 0; i < stepsToTake; i++)
                TakeTimeStep();
        }

        public string ToString()
        {
            return Moons[0].ToString() + Moons[1].ToString() + Moons[2].ToString() + Moons[3].ToString();
        }

        public long FindRepeatingPattern()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var patterWasFound = false;
            long stepsTaken = 0;
            while(!patterWasFound)
            {
                TakeTimeStep();
                stepsTaken++;
                //var moonsMatch = true;

                if (EnergyList.ContainsKey(ToString()))
                    return stepsTaken;
                EnergyList.Add(ToString(), 0);

                //for (var i = 0; i < Moons.Count; i++)
                //{
                //    if (!Moons[i].Equals(InitialMoonConfiguration[i]))
                //    {
                //        moonsMatch = false;
                //    }
                //if (moonsMatch)
                //    return stepsTaken;

                if (stepsTaken % 10000000 == 0)
                {
                    stopWatch.Stop();
                    Console.WriteLine($"Planetary system has run {stepsTaken} steps in {stopWatch.ElapsedMilliseconds / 1000} seconds");
                    stopWatch.Restart();
                }
            }
            return 0;
        }


        public long FindXRepeatingPattern()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            long stepsTaken = 0;
            while (true)
            {
                TakeTimeStep();
                stepsTaken++;
                Moons[0].XStepNumber = stepsTaken;
                Moons[1].XStepNumber = stepsTaken;
                Moons[2].XStepNumber = stepsTaken;
                Moons[3].XStepNumber = stepsTaken;
                if (Moons[0].Position.X == InitialMoonConfiguration[0].Position.X &&
                    Moons[1].Position.X == InitialMoonConfiguration[1].Position.X &&
                    Moons[2].Position.X == InitialMoonConfiguration[2].Position.X &&
                    Moons[3].Position.X == InitialMoonConfiguration[3].Position.X &&
                    Moons[0].Velocity.X == 0 &&
                    Moons[1].Velocity.X == 0 &&
                    Moons[2].Velocity.X == 0 &&
                    Moons[3].Velocity.X == 0)
                {
                    return stepsTaken;
                }

                if (stepsTaken % 10000000 == 0)
                {
                    stopWatch.Stop();
                    Console.WriteLine($"Planetary system has run {stepsTaken} steps in {stopWatch.ElapsedMilliseconds / 1000} seconds");
                    stopWatch.Restart();
                }
            }
        }

        public void TakeXStep()
        {
            for (var moonId = 0; moonId < 3; moonId++)
            {
                var moon = Moons[moonId];
                int moonDelta = 0;
                for (var moonToCompareId = 0; moonToCompareId < 3; moonToCompareId++)
                {
                    if (moonId != moonToCompareId)
                    {
                        var moonToCompare = Moons[moonToCompareId];
                        if (moon.Position.X < moonToCompare.Position.X)
                            moonDelta++;
                        if (moon.Position.X > moonToCompare.Position.X)
                            moonDelta--;
                    }
                }
                moon.Velocity.X += moonDelta;
            }

            Moons[3].Velocity.X = -1 * (Moons[0].Velocity.X + Moons[1].Velocity.X + Moons[2].Velocity.X);

            for (var moonId = 0; moonId < 4; moonId++)
            {
                Moons[moonId].Position.X += Moons[moonId].Velocity.X;
            }
        }

        public void TakeTimeStep()
        {
            for (var moonId = 0; moonId < 4; moonId++)
            {
                var moon = Moons[moonId];
                var moonDelta = new Coordinate(0, 0, 0);
                for (var moonToCompareId = 0; moonToCompareId < 4; moonToCompareId++)
                {
                    if (moonId != moonToCompareId)
                    {
                        var moonToCompare = Moons[moonToCompareId];
                        if (moon.Position.X < moonToCompare.Position.X)
                            moonDelta.X++;
                        if (moon.Position.X > moonToCompare.Position.X)
                            moonDelta.X--;
                        if (moon.Position.Y < moonToCompare.Position.Y)
                            moonDelta.Y++;
                        if (moon.Position.Y > moonToCompare.Position.Y)
                            moonDelta.Y--;
                        if (moon.Position.Z < moonToCompare.Position.Z)
                            moonDelta.Z++;
                        if (moon.Position.Z > moonToCompare.Position.Z)
                            moonDelta.Z--;
                    }
                }

                moon.StepDelta = moonDelta;
                moon.Velocity.X += moon.StepDelta.X;
                moon.Velocity.Y += moon.StepDelta.Y;
                moon.Velocity.Z += moon.StepDelta.Z;
            }

            foreach (var moon in Moons)
            {
                moon.Position.X += moon.Velocity.X;
                moon.Position.Y += moon.Velocity.Y;
                moon.Position.Z += moon.Velocity.Z;
            }

        }
    }
}
