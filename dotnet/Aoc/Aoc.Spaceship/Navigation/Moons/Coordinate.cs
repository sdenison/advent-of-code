using Aoc.Spaceship.Computer.Instructions;
using Math = System.Math;

namespace Aoc.Spaceship.Navigation.Moons
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Energy => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);

        public Coordinate(string coordinateString)
        {
            var coordinates = coordinateString.Split(',');
            X = int.Parse(coordinates[0].Split('=')[1]);
            Y = int.Parse(coordinates[1].Split('=')[1]);
            Z = int.Parse(coordinates[2].Split('=')[1].Replace('>', ' '));
        }

        public Coordinate(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
