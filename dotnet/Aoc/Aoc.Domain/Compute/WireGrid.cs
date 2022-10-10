using System;
using System.Collections.Generic;
using System.Text;

namespace Aoc.Domain.Compute
{
    public class WireGrid
    {
        private List<Move> _moves;
        public List<Coordinate> _grid;

        public Coordinate CoordinateAfterMove
        {
            get
            {
                var currentCoordinate = new Coordinate(1, 1);
                foreach (var move in _moves)
                {
                    switch (move.Direction)
                    {
                        case "R":
                            currentCoordinate.X +=  move.Distance;
                            break;
                        case "U":
                            currentCoordinate.Y +=  move.Distance;
                            break;
                        case "D":
                            currentCoordinate.Y -=  move.Distance;
                            break;
                        case "L":
                            currentCoordinate.X -=  move.Distance;
                            break;
                    }
                }

                return currentCoordinate;
            }
        }

        public WireGrid(List<string> moves)
        {
            _moves = new List<Move>();
            foreach (var move in moves)
                _moves.Add(new Move(move));
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
