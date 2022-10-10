using System;
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
                var wireAProgress = 0;
                var wireBProgress = 0;
                foreach (var coordinateA in _wireA.Path)
                {
                    wireAProgress++;
                    foreach (var coordinateB in _wireB.Path)
                    {
                        wireBProgress++;
                        if (coordinateA.Position.Equals(coordinateB.Position))
                        {
                            if (coordinateA.Direction == Direction.Left || coordinateA.Direction == Direction.Right)
                            {
                                if (coordinateB.Direction == Direction.Up || coordinateB.Direction == Direction.Down)
                                {
                                    intersections.Add(coordinateA.Position);
                                }
                            }
                            else
                            {
                                if (coordinateB.Direction == Direction.Left || coordinateB.Direction == Direction.Right)
                                {
                                    intersections.Add(coordinateA.Position);
                                }
                            }
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

//...........
//.+-----+...
//.|.....|...
//.|..+--X-+.
//.|..|..|.|.
//.|.-X--+.|.
//.|..|....|.
//.|.......|.
//.o-------+.
//...........
