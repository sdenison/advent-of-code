using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Aoc.Domain.Compute
{
    public class Wire
    {
        public List<Vector> Path { get; set; }
        public Coordinate CurrentCoordinate { get; set; }

        public Wire(List<Move> moves)
        {
            Path = new List<Vector>();
            CurrentCoordinate = new Coordinate(1, 1);
            GeneratePath(moves);
        }

        public Wire(string[] moves) : this(Move.ParseMoveList(moves))
        {
        }

        public void GeneratePath(List<Move> moves)
        {
            foreach (var move in moves)
            {
                Coordinate newCoordinate;
                switch (move.Direction)
                {
                    case "R":
                        for (int i = 1; i <= move.Distance; i++)
                        {
                            newCoordinate = new Coordinate(CurrentCoordinate.X + 1, CurrentCoordinate.Y);
                            Path.Add(new Vector(newCoordinate, Direction.Right));
                            CurrentCoordinate = newCoordinate;
                        }
                        break;
                    case "L":
                        for (int i = 1; i <= move.Distance; i++)
                        {
                            newCoordinate = new Coordinate(CurrentCoordinate.X - 1, CurrentCoordinate.Y);
                            Path.Add(new Vector(newCoordinate, Direction.Left));
                            CurrentCoordinate = newCoordinate;
                        }
                        break;
                    case "U":
                        for (int i = 1; i <= move.Distance; i++)
                        {
                            newCoordinate = new Coordinate(CurrentCoordinate.X, CurrentCoordinate.Y + 1);
                            Path.Add(new Vector(newCoordinate, Direction.Up));
                            CurrentCoordinate = newCoordinate;
                        }
                        break;
                    case "D":
                        for (int i = 1; i <= move.Distance; i++)
                        {
                            newCoordinate = new Coordinate(CurrentCoordinate.X, CurrentCoordinate.Y - 1);
                            Path.Add(new Vector(newCoordinate, Direction.Down));
                            CurrentCoordinate = newCoordinate;
                        }
                        break;
                    default:
                        throw new Exception();


                }
            }

        }
    }
}
