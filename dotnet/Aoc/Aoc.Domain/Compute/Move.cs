using System;
using System.Collections.Generic;
using System.Text;

namespace Aoc.Domain.Compute
{
    public class Move
    {
        public string Direction { get; set; }
        public int Distance { get; set; }

        public Move(string move)
        {
            Direction = move.Substring(0, 1);
            Distance = int.Parse(move.Substring(1, move.Length - 1));
        }

        public static List<Move> ParseMoveList(List<string> moveStrings)
        {
            var moves = new List<Move>();
            foreach (var moveString in moveStrings)
                moves.Add(new Move(moveString));
            return moves;
        }
    }
}
