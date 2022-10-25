using System.Collections.Generic;

namespace Aoc.Spaceship.Navigation
{
    public class Space
    {
        private char[,] _map;
        private int MapWidth => _map.GetLength(0);

        public Space(char[,] map)
        {
            _map = map;
        }

        public IList<Coordinate> GetVisibleAsteroids(Coordinate coordinate)
        {
            var maxOrbits = MaxOrbits(coordinate);
            for(var orbit = 0; orbit < maxOrbits; orbit++)
            {
            }
            var coordinates = new List<Coordinate>();
            return coordinates;
        }

        public int MaxOrbits(Coordinate coordinate)
        {
            var xSize= _map.GetLength(0);
            var ySize = _map.GetLength(1);
            var maxOrbits = coordinate.X;
            //if (maxOrbits < xSize - 1)
            if (maxOrbits < (xSize - coordinate.X) - 1)
                maxOrbits = (xSize - coordinate.X) - 1;
            if (maxOrbits < coordinate.Y)
                maxOrbits = coordinate.Y;
            if (maxOrbits < (ySize - coordinate.Y) - 1)
                maxOrbits = (ySize - coordinate.Y) - 1;
            return maxOrbits;
        }
    }
}
