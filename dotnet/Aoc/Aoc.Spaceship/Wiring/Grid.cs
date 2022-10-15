using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Aoc.Spaceship.Wiring
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
                var intersects = _wireA.Path.Keys.Intersect(_wireB.Path.Keys);
                foreach (var intersect in intersects)
                {
                    var wireASteps = _wireA.Path[intersect];
                    var wireBSteps = _wireB.Path[intersect];
                    intersections.AddRange(CompareSteps(wireASteps, wireBSteps));
                }

                return intersections;
            }
        }

        private IEnumerable<Intersection> CompareSteps(List<Step> wireASteps, List<Step> wireBSteps)
        {
            var intersections = (from wireAStep in wireASteps
                join wireBStep in wireBSteps on wireAStep.Position equals wireBStep.Position
                where wireAStep.Axis != wireBStep.Axis
                select new Intersection(wireAStep.Position, wireAStep.TotalStepsSoFar + wireBStep.TotalStepsSoFar));
            return intersections;
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
