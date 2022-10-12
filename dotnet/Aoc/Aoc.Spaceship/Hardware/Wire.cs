using System;
using System.Collections.Generic;

namespace Aoc.Spaceship.Hardware
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
                    case Directions.Right:
                        for (int i = 1; i <= move.Distance; i++)
                        {
                            newCoordinate = new Coordinate(CurrentCoordinate.X + 1, CurrentCoordinate.Y);
                            Path.Add(new Vector(newCoordinate, Axis.X));
                            CurrentCoordinate = newCoordinate;
                        }
                        break;
                    case Directions.Left:
                        for (int i = 1; i <= move.Distance; i++)
                        {
                            newCoordinate = new Coordinate(CurrentCoordinate.X - 1, CurrentCoordinate.Y);
                            Path.Add(new Vector(newCoordinate, Axis.X));
                            CurrentCoordinate = newCoordinate;
                        }
                        break;
                    case Directions.Up:
                        for (int i = 1; i <= move.Distance; i++)
                        {
                            newCoordinate = new Coordinate(CurrentCoordinate.X, CurrentCoordinate.Y + 1);
                            Path.Add(new Vector(newCoordinate, Axis.Y));
                            CurrentCoordinate = newCoordinate;
                        }
                        break;
                    case Directions.Down:
                        for (int i = 1; i <= move.Distance; i++)
                        {
                            newCoordinate = new Coordinate(CurrentCoordinate.X, CurrentCoordinate.Y - 1);
                            Path.Add(new Vector(newCoordinate, Axis.Y));
                            CurrentCoordinate = newCoordinate;
                        }
                        break;
                    default:
                        throw new InvalidHardware($"Direction unknown {move.Direction}");
                }
            }

        }
    }
}
