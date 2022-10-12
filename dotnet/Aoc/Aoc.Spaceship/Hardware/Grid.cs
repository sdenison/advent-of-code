using System;
using System.Collections.Generic;

namespace Aoc.Spaceship.Hardware
{
    public class Grid
    {
        private Wire _wireA;
        private Wire _wireB;

        public List<Intersection> Intersections
        {
            get
            {
                var intersections = new List<Intersection>();

                var wireADictionary = new Dictionary<Coordinate, List<Step>>();
                foreach (var step in _wireA.Path)
                {
                    if (wireADictionary.ContainsKey(step.Position))
                    {
                        wireADictionary[step.Position].Add(step);
                    }
                    else
                    {
                        var steps = new List<Step>() {step};
                        wireADictionary.Add(step.Position, steps);
                    }
                }

                var wireBSteps = 0;
                foreach (var stepB in _wireB.Path)
                {
                    wireBSteps++;
                    if (wireADictionary.ContainsKey(stepB.Position))
                    {
                        foreach (var stepA in wireADictionary[stepB.Position])
                            if (stepA.Axis != stepB.Axis)
                                intersections.Add(new Intersection(stepA.Position, stepA.TotalStepsSoFar + stepB.TotalStepsSoFar));
                    }
                }

                return intersections;
            }
        }

        public Grid(Wire wireA, Wire wireB)
        {
            _wireA = wireA;
            _wireB = wireB;

        }

        public static int GetManhattanDistance(Coordinate coordinateA, Coordinate coordinateB)
        {
            int xDistance = Math.Abs(coordinateA.X - coordinateB.X);
            int yDistance = Math.Abs(coordinateA.Y - coordinateB.Y);

            return xDistance + yDistance;
        }
    }
}
