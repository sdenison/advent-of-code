using System.Collections.Generic;

namespace Aoc.Spaceship.Wiring
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

        private void GeneratePath(List<Move> moves)
        {
            var totalStepsSoFar = 0;
            foreach (var move in moves)
            {
                totalStepsSoFar = MakeMove(move, totalStepsSoFar);
            }
        }

        private int MakeMove(Move move, int totalStepsSoFar)
        {
            for (var i = 0; i < move.Distance; i++)
            {
                totalStepsSoFar = TakeStep(move.Direction, totalStepsSoFar);
            }
            return totalStepsSoFar;
        }

        private int TakeStep(Direction direction, int totalStepsSoFar)
        {
            totalStepsSoFar++;
            switch (direction)
            {
                case Direction.Right:
                    CurrentCoordinate = new Coordinate(CurrentCoordinate.X + 1, CurrentCoordinate.Y);
                    Path.Add(new Step(CurrentCoordinate, Axis.X, totalStepsSoFar));
                    break;
                case Direction.Left:
                    CurrentCoordinate = new Coordinate(CurrentCoordinate.X - 1, CurrentCoordinate.Y);
                    Path.Add(new Step(CurrentCoordinate, Axis.X, totalStepsSoFar));
                    break;
                case Direction.Up:
                    CurrentCoordinate = new Coordinate(CurrentCoordinate.X, CurrentCoordinate.Y + 1);
                    Path.Add(new Step(CurrentCoordinate, Axis.Y, totalStepsSoFar));
                    break;
                case Direction.Down:
                    CurrentCoordinate = new Coordinate(CurrentCoordinate.X, CurrentCoordinate.Y - 1);
                    Path.Add(new Step(CurrentCoordinate, Axis.Y, totalStepsSoFar));
                    break;
                default:
                    throw new InvalidWiringConfiguration($"Direction unknown {direction}");
            }
            return totalStepsSoFar;
        }
    }
}
