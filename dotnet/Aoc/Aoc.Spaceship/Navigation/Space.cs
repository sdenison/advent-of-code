using System.Collections.Generic;
using System.Xml;

namespace Aoc.Spaceship.Navigation
{
    public class Space
    {
        private char[,] _map;
        private int MapWidth => _map.GetLength(0);
        private Coordinate _coordinateToCheck;

        public Space(char[,] map, Coordinate coordinateToCheck)
        {
            _map = map;
            _coordinateToCheck = coordinateToCheck;
        }

        public IList<Coordinate> GetVisibleAsteroids()
        {
            var coordinatesWithAsteroids = new List<Coordinate>();
            var maxOrbits = MaxOrbits(_coordinateToCheck);
            for(var orbit = 1; orbit <= maxOrbits; orbit++)
            {
                coordinatesWithAsteroids.AddRange(GetQuadrant1Asteroids(orbit));
            }
            return coordinatesWithAsteroids;
        }

        public IList<Coordinate> GetQuadrant1Asteroids(int orbit)
        {
            var coordinatesWithAsteroids = new List<Coordinate>();
            for(var y = 0; y < orbit; y++)
            {
                var x = orbit;
                var coordinateXy = new Coordinate(x, y);
                var translatedCoordinate = TranslatedCoordinateToMap(coordinateXy);
                if (_map[translatedCoordinate.X, translatedCoordinate.Y] == '#')
                    coordinatesWithAsteroids.Add(translatedCoordinate);
            }

            for(var x = 0; x < orbit; x++)
            {
                var y = orbit;
                var coordinateXy = new Coordinate(x, y);
                var translatedCoordinate = TranslatedCoordinateToMap(coordinateXy);
                if (_map[translatedCoordinate.X, translatedCoordinate.Y] == '#')
                    coordinatesWithAsteroids.Add(translatedCoordinate);
            }

            return coordinatesWithAsteroids;
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

        public Coordinate TranslatedCoordinateToMap(Coordinate coordinate)
        {
            return new Coordinate(coordinate.Y + _coordinateToCheck.X, coordinate.X + _coordinateToCheck.Y);
        }

        public Coordinate TranslatedCoordinateToXy(Coordinate coordinate)
        {
            return new Coordinate(coordinate.X + _coordinateToCheck.X, coordinate.Y + _coordinateToCheck.Y);
        }

        public char GetCoordinate(int x, int y)
        {
            return _map[y, x];
        }

        public char GetCoordinate(Coordinate coordinate)
        {
            return _map[coordinate.Y, coordinate.X];
        }
    }
}
