using System;
using System.Collections.Generic;
using System.IO;

namespace Aoc.Spaceship.Hardware
{
    public class Wire
    {
        public List<Step> Path { get; set; }
        public Coordinate CurrentCoordinate { get; set; }

        public Wire(List<Move> moves)
        {
            Path = new List<Step>();
            CurrentCoordinate = new Coordinate(1, 1);
            GeneratePath(moves);
        }

        public Wire(string[] moves) : this(Move.ParseMoveList(moves))
        {
        }

        public void GeneratePath(List<Move> moves)
        {
            var totalStepsSoFar = 0;
            foreach (var move in moves)
            {
                Coordinate newCoordinate;
                switch (move.Direction)
                {
                    case Directions.Right:
                        for (int i = 1; i <= move.Distance; i++)
                        {
                            totalStepsSoFar++;
                            newCoordinate = new Coordinate(CurrentCoordinate.X + 1, CurrentCoordinate.Y);
                            Path.Add(new Step(newCoordinate, Axis.X, totalStepsSoFar));
                            CurrentCoordinate = newCoordinate;
                        }
                        break;
                    case Directions.Left:
                        for (int i = 1; i <= move.Distance; i++)
                        {
                            totalStepsSoFar++;
                            newCoordinate = new Coordinate(CurrentCoordinate.X - 1, CurrentCoordinate.Y);
                            Path.Add(new Step(newCoordinate, Axis.X, totalStepsSoFar));
                            CurrentCoordinate = newCoordinate;
                        }
                        break;
                    case Directions.Up:
                        for (int i = 1; i <= move.Distance; i++)
                        {
                            totalStepsSoFar++;
                            newCoordinate = new Coordinate(CurrentCoordinate.X, CurrentCoordinate.Y + 1);
                            Path.Add(new Step(newCoordinate, Axis.Y, totalStepsSoFar));
                            CurrentCoordinate = newCoordinate;
                        }
                        break;
                    case Directions.Down:
                        for (int i = 1; i <= move.Distance; i++)
                        {
                            totalStepsSoFar++;
                            newCoordinate = new Coordinate(CurrentCoordinate.X, CurrentCoordinate.Y - 1);
                            Path.Add(new Step(newCoordinate, Axis.Y, totalStepsSoFar));
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
