using System.Collections.Generic;

namespace Aoc.Domain.Compute
{
    public class Grid
    {
        private Wire _wireA;
        private Wire _wireB;

        public List<Coordinate> Intersections
        {
            get
            {
                var intersections = new List<Coordinate>();
                foreach(var coordinateA in _wireA.Path)
                    foreach(var coordinateB in _wireB.Path)
                        if (coordinateA.Equals(coordinateB))
                            intersections.Add(coordinateA);
                return intersections;
            }
        }

        public Grid(Wire wireA, Wire wireB)
        {
            _wireA = wireA;
            _wireB = wireB;

        }
    }
}

//...........
//...........
//...........
//....+----+.
//....|....|.
//....|....|.
//....|....|.
//.........|.
//.o-------+.
//...........
