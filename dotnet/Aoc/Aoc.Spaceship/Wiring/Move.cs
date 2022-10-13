using System.Collections.Generic;

namespace Aoc.Spaceship.Wiring
{
    public class Move
    {
        public Direction Direction { get; set; }
        public int Distance { get; set; }

        public Move(string move)
        {
            Direction = ParseDirection(move); 
            Distance = int.Parse(move.Substring(1, move.Length - 1));
        }

        public static List<Move> ParseMoveList(string[] moveStrings)
        {
            var moves = new List<Move>();
            foreach (var moveString in moveStrings)
                moves.Add(new Move(moveString));
            return moves;
        }

        private Direction ParseDirection(string move)
        {
            var direction = move.Substring(0, 1).ToUpper();
            switch (direction)
            {
                case "U":
                    return Direction.Up;
                case "D":
                    return Direction.Down;
                case "R":
                    return Direction.Right;
                case "L":
                    return Direction.Left;
                default: throw new InvalidWiringConfiguration($"Unknown direction {direction}");
            }
        }
    }
}
