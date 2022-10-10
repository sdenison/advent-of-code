using System.Collections.Generic;

namespace Aoc.Domain.Compute
{
    public class Wire
    {
        public List<Coordinate> Path { get; set; }
        public Coordinate CurrentCoordinate { get; set; }

        public Wire(List<Move> moves)
        {
            Path = new List<Coordinate>();
            CurrentCoordinate = new Coordinate(1, 1);
            Path.Add(CurrentCoordinate);
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
                            Path.Add(newCoordinate);
                            CurrentCoordinate = newCoordinate;
                        }
                        break;
                    case "L":
                        for (int i = 1; i <= move.Distance; i++)
                        {
                            newCoordinate = new Coordinate(CurrentCoordinate.X - 1, CurrentCoordinate.Y);
                            Path.Add(newCoordinate);
                            CurrentCoordinate = newCoordinate;
                        }
                        break;
                    case "U":
                        for (int i = 1; i <= move.Distance; i++)
                        {
                            newCoordinate = new Coordinate(CurrentCoordinate.X, CurrentCoordinate.Y + 1);
                            Path.Add(newCoordinate);
                            CurrentCoordinate = newCoordinate;
                        }
                        break;
                    case "D":
                        for (int i = 1; i <= move.Distance; i++)
                        {
                            newCoordinate = new Coordinate(CurrentCoordinate.X, CurrentCoordinate.Y - 1);
                            Path.Add(newCoordinate);
                            CurrentCoordinate = newCoordinate;
                        }
                        break;

                }
            }

        }
    }
}
