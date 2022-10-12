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
                var wireASteps = 0;
                foreach (var coordinateA in _wireA.Path)
                {
                    wireASteps++;
                    var wireBSteps = 0;
                    foreach (var coordinateB in _wireB.Path)
                    {
                        wireBSteps++;
                        if (coordinateA.Position.Equals(coordinateB.Position))
                        {
                            if (coordinateA.Axis != coordinateB.Axis)
                                intersections.Add(new Intersection(coordinateA.Position, wireASteps + wireBSteps));
                        }
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
