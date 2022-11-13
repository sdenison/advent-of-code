using Aoc.Spaceship.Wiring;
using System;
using System.Collections.Generic;

namespace Aoc.Spaceship.Navigation.Moons
{
    public class AxisValue
    {
        public List<int> Positions { get; set;  }
        public List<int> Velocities { get; set;  }
        public long CurrentStep { get; set; }
        public SortedDictionary<long, SortedSet<long>> StepHistory { get; set; }
        public List<int> PositionHistory { get; set; }
        public List<Tuple<int, int, int, int, long>> CoordinateHistory { get; set; }
        public List<AxisSnapshot> AxisSnapshots { get; set; }




        public AxisValue(int position0, int position1, int position2, int position3)
        {
            Positions = new List<int>()
            {
                position0,
                position1,
                position2,
                position3,
            };
            Velocities = new List<int>
            {
                0,
                0,
                0,
                0,
            };
            CurrentStep = 0;
            StepHistory = new SortedDictionary<long, SortedSet<long>>();
            PositionHistory = new List<int>();
            AddToStepHisotry();
            CoordinateHistory = new List<Tuple<int, int, int, int, long>>();
            AxisSnapshots = new List<AxisSnapshot>();
            //CoordinateHistory.Add(new Tuple<int, int, int, int>(position0, position1, position2, position3));
        }

        public AxisValue(int position0, int position1, int position2, int position3, int velocity0, int velocity1, int velocity2, int velocity3, long currentStep)
        {
            Positions = new List<int>()
            {
                position0,
                position1,
                position2,
                position3,
            };
            Velocities = new List<int>
            {
                velocity0,
                velocity1,
                velocity2,
                velocity3,
            };
            CurrentStep = currentStep;
            StepHistory = new SortedDictionary<long, SortedSet<long>>();
        }

        public long GetLong()
        {
            return Math.Abs(Positions[0]) * 100000000000000 + Math.Abs(Positions[1]) * 1000000000000 + Math.Abs(Positions[2]) * 10000000000 + Math.Abs(Positions[3]) * 100000000 +
                   Math.Abs(Velocities[0]) * 1000000 + Math.Abs(Velocities[1]) * 10000 + Math.Abs(Velocities[2]) * 100 + Math.Abs(Velocities[3]);
        }


        public void TakeSteps(long stepsToTake)
        {
            PositionHistory.Clear();
            CoordinateHistory.Clear();
            AxisSnapshots.Clear();
            for(var i = 0; i < stepsToTake; i++)
                TakeStep();
        }

        public void TakeStep()
        {
            for (var moonId = 0; moonId < 4; moonId++)
            {
                var position = Positions[moonId];
                for (var moonToCompareId = 0; moonToCompareId < 4; moonToCompareId++)
                {
                    if (moonId != moonToCompareId)
                    {
                        var moonToCompare = Positions[moonToCompareId];
                        if (position < moonToCompare)
                            Velocities[moonId]++;
                        if (position > moonToCompare)
                            Velocities[moonId]--;
                    }
                }
            }
            for (var moonId = 0; moonId < 4; moonId++)
            {
                Positions[moonId] += Velocities[moonId];
            }
            CurrentStep++;
            CoordinateHistory.Add(new Tuple<int, int, int, int, long>(Positions[0], Positions[1], Positions[2], Positions[3], CurrentStep));
            AxisSnapshots.Add(new AxisSnapshot{ Position0 = Positions[0], Position1 = Positions[1], Position2 = Positions[2], Position3 = Positions[3], 
                Velocity0 = Velocities[0], Velocity1 = Velocities[1], Velocity2 = Velocities[2], Velocity3 = Velocities[3],
                StepNumber = CurrentStep});
        }

        public void AddToPositionHistory()
        {
            PositionHistory.Add(Math.Abs(Positions[0]) + Positions[1] * 100 + Positions[2] * 10000 + Positions[3] * 1000000);
        }

        public bool AddToStepHisotry()
        {
            if (StepHistory.ContainsKey(GetLong()))
            {
                StepHistory[GetLong()].Add(CurrentStep);
                return true;
            }
            StepHistory.Add(GetLong(), new SortedSet<long>(){ CurrentStep });
            return false;
        }
    }
}
