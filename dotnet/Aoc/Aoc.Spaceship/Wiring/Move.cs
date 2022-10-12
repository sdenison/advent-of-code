using System.Collections.Generic;

namespace Aoc.Spaceship.Hardware
{
    public class Move
    {
        public Directions Direction { get; set; }
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

        private Directions ParseDirection(string move)
        {
            var direction = move.Substring(0, 1).ToUpper();
            switch (direction)
            {
                case "U":
                    return Directions.Up;
                case "D":
                    return Directions.Down;
                case "R":
                    return Directions.Right;
                case "L":
                    return Directions.Left;
                default: throw new InvalidHardware($"Unknown direction {direction}");
            }
        }

    }
}
