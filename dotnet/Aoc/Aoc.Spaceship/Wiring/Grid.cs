using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.Spaceship.Wiring
{
    public class Grid
    {
        private Wire _greenWire;
        private Wire _redWire;

        public Grid(Wire greenWire, Wire redWire)
        {
            _greenWire = greenWire;
            _redWire = redWire;
        }

        public List<Intersection> Intersections
        {
            get
            {
                var intersections = new List<Intersection>();
                var intersects = _greenWire.Path.Keys.Intersect(_redWire.Path.Keys);
                foreach (var intersect in intersects)
                {
                    var wireASteps = _greenWire.Path[intersect];
                    var wireBSteps = _redWire.Path[intersect];
                    intersections.AddRange(CompareSteps(wireASteps, wireBSteps));
                }

                return intersections;
            }
        }

        private IEnumerable<Intersection> CompareSteps(List<Step> greenWireSteps, List<Step> redWireSteps)
        {
            var intersections = (from greenWireStep in greenWireSteps
                join redWireStep in redWireSteps on greenWireStep.Position equals redWireStep.Position
                where greenWireStep.Axis != redWireStep.Axis
                select new Intersection(redWireStep.Position, greenWireStep.TotalStepsSoFar + redWireStep.TotalStepsSoFar));
            return intersections;
        }

        public static int GetManhattanDistance(Coordinate coordinateA, Coordinate coordinateB)
        {
            int xDistance = Math.Abs(coordinateA.X - coordinateB.X);
            int yDistance = Math.Abs(coordinateA.Y - coordinateB.Y);
            return xDistance + yDistance;
        }
    }
}
