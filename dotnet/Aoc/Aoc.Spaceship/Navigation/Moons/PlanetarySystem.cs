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
        //public SortedDictionary<string, int> EnergyList { get; set; }
        public History History { get; set; }
        public int _xsum = 0;
        public int _ysum = 0;
        public int _zsum = 0;

        public SortedDictionary<Vector, SortedDictionary<Vector, SortedDictionary<Vector, List<Vector>>>> VectorHistory
        {
            get;
            set;
        }


        public int TotalEnergy
        {
            get { return Moons.Select(x => x.TotalEnergy).Sum(); }
        }

        public PlanetarySystem(IList<string> moonCoordinates)
        {
            VectorHistory = new SortedDictionary<Vector, SortedDictionary<Vector, SortedDictionary<Vector, List<Vector>>>>();
            //History = new SortedSet<Snapshot>();
            History = new History();
            Moons = new List<Moon>();
            InitialMoonConfiguration = new List<Moon>();
            foreach(var moon in moonCoordinates)
            {
                Moons.Add(new Moon(moon));
                InitialMoonConfiguration.Add(new Moon(moon));
            }

            _xsum = Moons[0].Position.X + Moons[1].Position.X + Moons[2].Position.X + Moons[3].Position.X;
            _ysum = Moons[0].Position.Y + Moons[1].Position.Y + Moons[2].Position.Y + Moons[3].Position.Y;
            _zsum = Moons[0].Position.Z + Moons[1].Position.Z + Moons[2].Position.Z + Moons[3].Position.Z;

            AddToVectorHistory();
        }

        public bool AddToVectorHistory()
        {
            var vector0 = new Vector(Moons[0].Position.X, Moons[0].Position.Y, Moons[0].Position.Z, Moons[0].Velocity.X, Moons[0].Velocity.Y, Moons[0].Velocity.Z);
            var vector1 = new Vector(Moons[1].Position.X, Moons[1].Position.Y, Moons[1].Position.Z, Moons[1].Velocity.X, Moons[1].Velocity.Y, Moons[1].Velocity.Z);
            var vector2 = new Vector(Moons[2].Position.X, Moons[2].Position.Y, Moons[2].Position.Z, Moons[2].Velocity.X, Moons[2].Velocity.Y, Moons[2].Velocity.Z);
            //var vector3 = new Vector(Moons[3].Position.X, Moons[3].Position.Y, Moons[3].Position.Z, Moons[3].Velocity.X, Moons[3].Velocity.Y, Moons[3].Velocity.Z);
            //var snapshot = new Snapshot(vector0, vector1, vector2, vector3);
            var snapshot = new Snapshot(vector0, vector1, vector2);
            if (History.AddSnapshot(snapshot))
                return false;
            return true;
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

        public long ToInt()
        {
            var _1hash = Moons[0].GetHashCode();
            var _2hash = Moons[2].GetHashCode();
            
            var x = Tuple.Create(Moons[0].GetHash(), Moons[1].GetHash(), Moons[2].GetHash(), Moons[3].GetHash());
            return x.GetHashCode();
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

                if (!AddToVectorHistory())
                    return stepsTaken;

                //for (var i = 0; i < Moons.Count; i++)
                //{
                //    if (!Moons[i].Equals(InitialMoonConfiguration[i]))
                //    {
                //        moonsMatch = false;
                //    }
                //if (moonsMatch)
                //    return stepsTaken;

                if (stepsTaken % 1000000 == 0)
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
                            moon.Velocity.X++;
                        if (moon.Position.X > moonToCompare.Position.X)
                            moon.Velocity.X--;
                        if (moon.Position.Y < moonToCompare.Position.Y)
                            moon.Velocity.Y++;
                        if (moon.Position.Y > moonToCompare.Position.Y)
                            moon.Velocity.Y--;
                        if (moon.Position.Z < moonToCompare.Position.Z)
                            moon.Velocity.Z++;
                        if (moon.Position.Z > moonToCompare.Position.Z)
                            moon.Velocity.Z--;
                    }
                }
            }

            for (var moonId = 0; moonId < 3; moonId++)
            {
                var moon = Moons[moonId];
                moon.Position.X += moon.Velocity.X;
                moon.Position.Y += moon.Velocity.Y;
                moon.Position.Z += moon.Velocity.Z;
            }

            Moons[3].Position.X = _xsum - (Moons[0].Position.X + Moons[1].Position.X + Moons[2].Position.X);
            Moons[3].Position.Y = _ysum - (Moons[0].Position.Y + Moons[1].Position.Y + Moons[2].Position.Y);
            Moons[3].Position.Z = _zsum - (Moons[0].Position.Z + Moons[1].Position.Z + Moons[2].Position.Z);
        }
    }
}
