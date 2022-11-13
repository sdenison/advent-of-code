using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Aoc.Spaceship.Navigation.Moons
{
    public class PlanetarySystem
    {
        public IList<Moon> Moons { get; set; }
        public IList<Moon> InitialMoonConfiguration { get; set; }
        //public SortedDictionary<string, int> EnergyList { get; set; }
        public History History { get; set; }
        public AxisValue XAxis { get; set; }
        public SortedDictionary<long, List<long>> XAxisHistory; 
        public AxisValue YAxis { get; set; }
        public SortedDictionary<long, List<long>> YAxisHistory; 
        public AxisValue ZAxis { get; set; }
        public SortedDictionary<long, List<long>> ZAxisHistory; 
        public SortedDictionary<long, SortedSet<long>> XAxisSnapshots { get; set; }
        public SortedSet<int> Positions { get; set; }

        public SortedSet<long> StepHistory { get; set; }


        public int TotalEnergy()
        {
            var totalEnergy = 0;
            for(var i = 0; i < 4; i++)
            {
                var pot = Math.Abs(XAxis.Positions[i]) + Math.Abs(YAxis.Positions[i]) + Math.Abs(ZAxis.Positions[i]);
                var kin = Math.Abs(XAxis.Velocities[i]) + Math.Abs(YAxis.Velocities[i]) + Math.Abs(ZAxis.Velocities[i]);
                totalEnergy += pot * kin;
            }
            return totalEnergy;
        }

        public PlanetarySystem(IList<string> moonCoordinates)
        {
            XAxisSnapshots = new SortedDictionary<long, SortedSet<long>>();

            History = new History();
            Moons = new List<Moon>();
            InitialMoonConfiguration = new List<Moon>();

            StepHistory = new SortedSet<long>();

            
            foreach(var moon in moonCoordinates)
            {
                Moons.Add(new Moon(moon));
                InitialMoonConfiguration.Add(new Moon(moon));
            }

            XAxis = new AxisValue(Moons[0].Position.X, Moons[1].Position.X, Moons[2].Position.X, Moons[3].Position.X);
            XAxisHistory = new SortedDictionary<long, List<long>>();
            YAxis = new AxisValue(Moons[0].Position.Y, Moons[1].Position.Y, Moons[2].Position.Y, Moons[3].Position.Y);
            YAxisHistory = new SortedDictionary<long, List<long>>();
            ZAxis = new AxisValue(Moons[0].Position.Z, Moons[1].Position.Z, Moons[2].Position.Z, Moons[3].Position.Z);
            ZAxisHistory = new SortedDictionary<long, List<long>>();
            Positions = new SortedSet<int>();

            AddToXaxisHisotry();
        }

        public bool AddToXaxisHisotry()
        {
            if (XAxisHistory.ContainsKey(XAxis.GetLong()))
            {
                XAxisHistory[XAxis.GetLong()].Add(XAxis.CurrentStep);
                return true;
            }
            XAxisHistory.Add(XAxis.GetLong(), new List<long>{ XAxis.CurrentStep });
            return false;
        }

        public bool AddToYaxisHisotry()
        {
            if (YAxisHistory.ContainsKey(YAxis.GetLong()))
            {
                YAxisHistory[YAxis.GetLong()].Add(YAxis.CurrentStep);
                return true;
            }
            YAxisHistory.Add(YAxis.GetLong(), new List<long> { YAxis.CurrentStep });
            return false;
        }

        public bool AddToZaxisHisotry()
        {
            if (ZAxisHistory.ContainsKey(ZAxis.GetLong()))
            {
                ZAxisHistory[ZAxis.GetLong()].Add(ZAxis.CurrentStep);
                return true;
            }
            ZAxisHistory.Add(ZAxis.GetLong(), new List<long> { ZAxis.CurrentStep });
            return false;
        }

        public void TakeTimeSteps(int stepsToTake)
        {
            XAxis.TakeSteps(stepsToTake);
            YAxis.TakeSteps(stepsToTake);
            ZAxis.TakeSteps(stepsToTake);
        }

        public long FindRepeatingPattern()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var patterWasFound = false;
            var returnXSteps = new SortedSet<long>();
            long returnXStep = 0;
            var returnYSteps = new SortedSet<long>();
            long returnYStep = 0;
            var returnZSteps = new SortedSet<long>();
            long returnZStep = 0;
            var xyCombinations = new List<long>();
            while(!patterWasFound)
            {
                long currentStep = 1;
                var stepSize = 5000; //10000 was 8 seconds;  100000 was 9 seconds
                long returnStep = 0;

                ////Console.WriteLine("Calculating steps...");
                //Task[] taskArray = new Task[3];
                //if (!returnXSteps.Any())
                //    taskArray[0] = Task.Run(() => XAxis.TakeSteps(stepSize));
                //else
                //    taskArray[0] = Task.CompletedTask;
                //if (!returnYSteps.Any())
                //    taskArray[1] = Task.Run(() => YAxis.TakeSteps(stepSize));
                //else
                //    taskArray[1] = Task.CompletedTask;
                //if (!returnZSteps.Any())
                //    taskArray[2] = Task.Run(() => ZAxis.TakeSteps(stepSize));
                //else
                //    taskArray[2] = Task.CompletedTask;

                //Console.WriteLine("Calculating steps...");
                if (returnXStep == 0)
                    XAxis.TakeSteps(stepSize);
                if (returnYStep == 0)
                    YAxis.TakeSteps(stepSize);
                if (returnZStep == 0)
                    ZAxis.TakeSteps(stepSize);

                //stepsTaken++;
                //Task.WaitAll(taskArray);

                if (returnXStep == 0)
                    for (int i = 0; i < stepSize; i++)
                    {
                        if (XAxis.AxisSnapshots[i].Position0 == Moons[0].Position.X &&
                            XAxis.AxisSnapshots[i].Position1 == Moons[1].Position.X &&
                            XAxis.AxisSnapshots[i].Position2 == Moons[2].Position.X &&
                            XAxis.AxisSnapshots[i].Position3 == Moons[3].Position.X &&
                            XAxis.AxisSnapshots[i].Velocity0 == 0 &&
                            XAxis.AxisSnapshots[i].Velocity1 == 0 &&
                            XAxis.AxisSnapshots[i].Velocity2 == 0 &&
                            XAxis.AxisSnapshots[i].Velocity3 == 0)
                        {
                            returnXSteps.Add(XAxis.AxisSnapshots[i].StepNumber);
                            returnXStep = XAxis.AxisSnapshots[i].StepNumber;
                            Console.WriteLine($"Found X step of {returnXStep}");
                            break;
                        }
                    }

                if (returnYStep == 0)
                    for (int i = 0; i < stepSize; i++)
                    {
                        if (YAxis.AxisSnapshots[i].Position0 == Moons[0].Position.Y &&
                            YAxis.AxisSnapshots[i].Position1 == Moons[1].Position.Y &&
                            YAxis.AxisSnapshots[i].Position2 == Moons[2].Position.Y &&
                            YAxis.AxisSnapshots[i].Position3 == Moons[3].Position.Y &&
                            YAxis.AxisSnapshots[i].Velocity0 == 0 &&
                            YAxis.AxisSnapshots[i].Velocity1 == 0 &&
                            YAxis.AxisSnapshots[i].Velocity2 == 0 &&
                            YAxis.AxisSnapshots[i].Velocity3 == 0)
                        {
                            returnYSteps.Add(YAxis.AxisSnapshots[i].StepNumber);
                            returnYStep = YAxis.AxisSnapshots[i].StepNumber;
                            Console.WriteLine($"Found Y step of {XAxis.AxisSnapshots[i].StepNumber}");
                            break;
                        }
                    }

                if (returnZStep == 0)
                    for (int i = 0; i < stepSize; i++)
                    {
                        if (ZAxis.AxisSnapshots[i].Position0 == Moons[0].Position.Z &&
                            ZAxis.AxisSnapshots[i].Position1 == Moons[1].Position.Z &&
                            ZAxis.AxisSnapshots[i].Position2 == Moons[2].Position.Z &&
                            ZAxis.AxisSnapshots[i].Position3 == Moons[3].Position.Z &&
                            ZAxis.AxisSnapshots[i].Velocity0 == 0 &&
                            ZAxis.AxisSnapshots[i].Velocity1 == 0 &&
                            ZAxis.AxisSnapshots[i].Velocity2 == 0 &&
                            ZAxis.AxisSnapshots[i].Velocity3 == 0)
                        {
                            returnZSteps.Add(ZAxis.AxisSnapshots[i].StepNumber);
                            returnZStep = YAxis.AxisSnapshots[i].StepNumber;
                            Console.WriteLine($"Found Z step of {XAxis.AxisSnapshots[i].StepNumber}");
                            break;
                        }
                    }


                if (returnXStep > 0 && returnYStep > 0 && returnZStep > 0)
                {
                    //var xSteps = returnXSteps.Min();
                    //var ySteps = returnYSteps.Min();
                    //var zSteps = returnZSteps.Min();
                    var lcm = PlanetarySystem.lcm(returnXStep, returnYStep);
                    lcm = PlanetarySystem.lcm(lcm, returnZStep);
                    return lcm;
                }


                //for (int i = 0; i < stepSize; i++)
                //{
                //    position += XAxis.Positions[i] * IntPow(10, i) + YAxis.Positions[i] * IntPow(10, i) + ZAxis.Positions[i] * IntPow(10, i);
                //}

                //position = ulong.Parse(ZAxis.Positions[3]);

                //Console.WriteLine("Looking for matches...");
                //Parallel.For(0, stepSize, (i) =>
                //{
                //    if (returnXSteps.Count == 0)
                //        if (XAxis.CoordinateHistory[i].Item1 == Moons[0].Position.X &&
                //            XAxis.CoordinateHistory[i].Item2 == Moons[1].Position.X &&
                //            XAxis.CoordinateHistory[i].Item2 == Moons[2].Position.X &&
                //            XAxis.CoordinateHistory[i].Item2 == Moons[3].Position.X
                //            )



                //    if (XAxis.CoordinateHistory[i].Item1 == Moons[0].Position.X &&
                //        YAxis.CoordinateHistory[i].Item1 == Moons[0].Position.Y &&
                //        ZAxis.CoordinateHistory[i].Item1 == Moons[0].Position.Z &&
                //        XAxis.CoordinateHistory[i].Item2 == Moons[1].Position.X &&
                //        YAxis.CoordinateHistory[i].Item2 == Moons[1].Position.Y &&
                //        ZAxis.CoordinateHistory[i].Item2 == Moons[1].Position.Z &&
                //        XAxis.CoordinateHistory[i].Item3 == Moons[2].Position.X &&
                //        YAxis.CoordinateHistory[i].Item3 == Moons[2].Position.Y &&
                //        ZAxis.CoordinateHistory[i].Item3 == Moons[2].Position.Z &&
                //        XAxis.CoordinateHistory[i].Item4 == Moons[3].Position.X &&
                //        YAxis.CoordinateHistory[i].Item4 == Moons[3].Position.Y &&
                //        ZAxis.CoordinateHistory[i].Item4 == Moons[3].Position.Z)
                //        returnXSteps.Add(XAxis.CoordinateHistory[i].Item5);
                //    //returnStep = XAxis.CoordinateHistory[i].Item5;

                //});

                //if (returnXSteps.Count > 0)
                //    return returnXSteps.AsQueryable().Min();


                //if (AddToXaxisHisotry())
                //{
                //If we're here then we just added a match for X
                //Console.WriteLine($"Found X axis match on step {XAxis.CurrentStep}. XAxisHistory is {XAxisHistory.Count} large");
                //}

                //AddToYaxisHisotry();
                //AddToZaxisHisotry();

                if (XAxis.CurrentStep % 100000000 == 0)
                {
                    stopWatch.Stop();
                    var ellapsedTime = String.Format("{0:n0}", (stopWatch.ElapsedMilliseconds / 1000));
                    var currentStepString = String.Format("{0:n0}", XAxis.CurrentStep);
                    Console.WriteLine($"Planetary system has run {currentStepString} steps in {ellapsedTime} seconds");
                    //Console.WriteLine($"Positions has {Positions.Count} records");
                    //Console.WriteLine($"XAxisHistory is {XAxis.StepHistory.Count} large");
                    //Console.WriteLine($"YAxisHistory is {YAxis.StepHistory.Count} large");
                    //Console.WriteLine($"ZAxisHistory is {ZAxis.StepHistory.Count} large");
                    stopWatch.Restart();
                }
            }
            return 0;
        }

        public long GetFirstDuplicate()
        {
            foreach(var xStepHistory in XAxis.StepHistory.Values)
            {
                foreach(var yStepHistory in YAxis.StepHistory.Values)
                {
                    var yIntersects = xStepHistory.Intersect(yStepHistory);
                    if (yIntersects.Count() > 1)
                    {
                        foreach (var zStepHistory in ZAxis.StepHistory.Values)
                        {
                            var zIntersects = zStepHistory.Intersect(yIntersects);
                            {
                                if (zIntersects.Count() > 1)
                                    return zIntersects.ElementAt(1);
                            }
                        }
                    }

                }
            }

            return 0;
        }

        long PowTen(int pow)
        {
            long ret = 1;
            long ten = 10;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    ret *= 10;
                ten *= 10;
                pow >>= 1;
            }
            return ret;
        }

        int IntPow(int x, int pow)
        {
            int ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }

        static long gcf(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static long lcm(long a, long b)
        {
            return (a / gcf(a, b)) * b;
        }
    }
}
