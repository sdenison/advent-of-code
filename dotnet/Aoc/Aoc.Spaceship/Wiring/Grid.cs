using System;
using System.Collections.Generic;

namespace Aoc.Spaceship.Wiring
{
    public class Grid
    {
        private Wire _wireA;
        private Wire _wireB;

        private Dictionary<Coordinate, List<Step>> WireADictionary
        {
            get
            {
                var wireADictionary = new Dictionary<Coordinate, List<Step>>();
                foreach (var step in _wireA.Path)
                    if (wireADictionary.ContainsKey(step.Position))
                        wireADictionary[step.Position].Add(step);
                    else
                        wireADictionary.Add(step.Position, new List<Step> {step});
                return wireADictionary;
            }
        }

        public List<Intersection> Intersections
        {
            get
            {
                //This is optimized for speed so it's hurting readability a bit
                //I needed the speed to be improved so the unit test could run
                //Get a Dictionary of the steps in wire A
                //Loop only through wire B and look for values in the wire A dictionary
                //This is like adding an index to a table
                //It took the method from 10 minutes to under a second
                var intersections = new List<Intersection>();
                var wireADictionary = WireADictionary;
                foreach (var stepB in _wireB.Path)
                    if (wireADictionary.ContainsKey(stepB.Position))
                        foreach (var stepA in wireADictionary[stepB.Position])
                            if (stepA.Axis != stepB.Axis)
                                intersections.Add(new Intersection(stepA.Position, stepA.TotalStepsSoFar + stepB.TotalStepsSoFar));
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
