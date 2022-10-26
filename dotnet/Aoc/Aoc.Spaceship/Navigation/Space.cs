using System;
using System.Collections.Generic;
using System.Linq;
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

        public int XMax()
        {
            return _map.GetLength(0) - (_coordinateToCheck.X + 1);
        }

        public int XMin()
        {
            return -1 * (_map.GetLength(0) - (XMax() + 1));
        }

        public int YMin()
        {
            return -1 * (_map.GetLength(1) - (_coordinateToCheck.Y + 1));
        }

        public int YMax()
        {
            return (_map.GetLength(1) - 1) + YMin();
        }

        public IList<Coordinate> GetAllAsteroids()
        {
            var coordinatesWithAsteroids = new List<Coordinate>();
            var maxOrbits = MaxOrbits(_coordinateToCheck);
            for(var orbit = 1; orbit <= maxOrbits; orbit++)
            {
                coordinatesWithAsteroids.AddRange(GetQuadrant1Asteroids(orbit));
                coordinatesWithAsteroids.AddRange(GetQuadrant2Asteroids(orbit));
                coordinatesWithAsteroids.AddRange(GetQuadrant3Asteroids(orbit));
                coordinatesWithAsteroids.AddRange(GetQuadrant4Asteroids(orbit));
            }
            return coordinatesWithAsteroids;
        }

        public IList<Coordinate> RotateLaser()
        {
            var blastedCoordinates = new List<Coordinate>();
            var maxOrbits = MaxOrbits(_coordinateToCheck);
            var allAsteroids = GetAllAsteroids();
            for (var orbit = 1; orbit <= maxOrbits; orbit++)
            {
                blastedCoordinates.AddRange(BlastQuadrant1(allAsteroids));
                blastedCoordinates.AddRange(BlastQuadrant4(allAsteroids));
                blastedCoordinates.AddRange(BlastQuadrant3(allAsteroids));
                blastedCoordinates.AddRange(BlastQuadrant2(allAsteroids));
            }
            return blastedCoordinates;
        }

        public IList<Coordinate> BlastQuadrant1(IList<Coordinate> asteroids)
        {
            var blastedAsteroids = new List<Coordinate>();
            //Look straight up
            var hasValueStraightUp = asteroids.Any(x => x.Quadrant == 2 && !x.Slope.HasValue);
            if (hasValueStraightUp)
            {
                var asteroidStraightUp = asteroids.First(x => x.Quadrant == 2 && !x.Slope.HasValue);
                blastedAsteroids.Add(asteroidStraightUp);
                asteroids.Remove(asteroidStraightUp);
            }

            var quadrent1s = asteroids.Where(x => x.Quadrant == 1).OrderByDescending(x => Math.Abs(x.Slope.Value));
            foreach (var quadrent1 in quadrent1s)
            {
                var x = quadrent1.X;
                var y = quadrent1.Y;
                var quadrent = quadrent1.Quadrant;
            }
                

            var slopes = asteroids.Where(x => x.Quadrant == 1).OrderByDescending(x => Math.Abs(x.Slope.Value))
                .GroupBy(x => Math.Abs(x.Slope.Value)).Select(x => x.First().Slope);
            foreach(var slope in slopes)
            {
                var asteroidToBlast = asteroids.First(x => x.Quadrant == 1 && x.Slope.Value == slope);
                blastedAsteroids.Add(asteroidToBlast);
                asteroids.Remove(asteroidToBlast);
            }
            return blastedAsteroids;
        }

        public IList<Coordinate> BlastQuadrant4(IList<Coordinate> asteroids)
        {
            var blastedAsteroids = new List<Coordinate>();

            return blastedAsteroids;
        }

        public IList<Coordinate> BlastQuadrant3(IList<Coordinate> asteroids)
        {
            var blastedAsteroids = new List<Coordinate>();

            return blastedAsteroids;
        }

        public IList<Coordinate> BlastQuadrant2(IList<Coordinate> asteroids)
        {
            var blastedAsteroids = new List<Coordinate>();

            return blastedAsteroids;
        }

        public IList<Coordinate> GetVisibleAsteroids()
        {
            var visibleAsteroids = new List<Coordinate>();
            var allAsteroids = GetAllAsteroids();
            foreach (var asteroid in allAsteroids)
            {
                if (!visibleAsteroids.Any(x => x.Quadrant == asteroid.Quadrant && x.Slope == asteroid.Slope))
                    visibleAsteroids.Add(asteroid);
            }
            return visibleAsteroids;
        }

        public Coordinate FindBestLocation()
        {
            var maxVisibleAsteroids = 0;
            Coordinate bestCoordinate = null;
            for (var x = 0; x < _map.GetLength(0); x++)
                for (var y = 0; y < _map.GetLength(1); y++)
                {
                    if (x == 5 && y == 8)
                    {
                        var z = "let's see";
                    }
                    if ('#' == GetValueAtMapCoordinate(x, y))
                    {
                        _coordinateToCheck = new Coordinate(x, y);
                        var visibleAsteroids = GetVisibleAsteroids().Count;
                        if (visibleAsteroids > maxVisibleAsteroids)
                        {
                            maxVisibleAsteroids = visibleAsteroids;
                            bestCoordinate = _coordinateToCheck;
                            bestCoordinate.VisibleAsteroids = visibleAsteroids;
                        }
                    }
                }

            return bestCoordinate;
        }

        public IList<Coordinate> GetQuadrant1Asteroids(int orbit)
        {
            var coordinatesWithAsteroids = new List<Coordinate>();
            for(var y = 0; y <= orbit; y++)
            {
                var x = orbit;
                var coordinateXy = new Coordinate(x, y);
                try
                {
                    var charToTest = GetValueAtXyCoordinate(coordinateXy);
                    if (charToTest == '#')
                        coordinatesWithAsteroids.Add(coordinateXy);
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }

            for(var x = 1; x < orbit; x++)
            {
                var y = orbit;
                var coordinateXy = new Coordinate(x, y);
                try
                {
                    var charToTest = GetValueAtXyCoordinate(coordinateXy);
                    if (charToTest == '#')
                        coordinatesWithAsteroids.Add(coordinateXy);
                }catch(IndexOutOfRangeException)
                {
                    break;
                }
            }

            return coordinatesWithAsteroids;
        }

        public IList<Coordinate> GetQuadrant2Asteroids(int orbit)
        {
            var coordinatesWithAsteroids = new List<Coordinate>();
            for (var y = 1; y <= orbit; y++)
            {
                var x = -1 * orbit;
                var coordinateXy = new Coordinate(x, y);
                try
                {
                    var charToTest = GetValueAtXyCoordinate(coordinateXy);
                    if (charToTest == '#')
                        coordinatesWithAsteroids.Add(coordinateXy);
                }catch(IndexOutOfRangeException)
                {
                    break;
                }
            }

            for (var x = 0; x > -1 * orbit; x--)
            {
                var y = orbit;
                var coordinateXy = new Coordinate(x, y);
                try
                {
                    var charToTest = GetValueAtXyCoordinate(coordinateXy);
                    if (charToTest == '#')
                        coordinatesWithAsteroids.Add(coordinateXy);
                }catch(IndexOutOfRangeException)
                {
                    break;
                }
            }

            return coordinatesWithAsteroids;
        }

        public IList<Coordinate> GetQuadrant3Asteroids(int orbit)
        {
            var coordinatesWithAsteroids = new List<Coordinate>();
            for (var y = 0; y >= -1 * orbit; y--)
            {
                var x = -1 * orbit;
                var coordinateXy = new Coordinate(x, y);
                try
                {
                    var charToTest = GetValueAtXyCoordinate(coordinateXy);
                    if (charToTest == '#')
                        coordinatesWithAsteroids.Add(coordinateXy);
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }

            for (var x = -1; x > -1 * orbit; x--)
            {
                var y = -1 * orbit;
                var coordinateXy = new Coordinate(x, y);
                try
                {
                    var charToTest = GetValueAtXyCoordinate(coordinateXy);
                    if (charToTest == '#')
                        coordinatesWithAsteroids.Add(coordinateXy);
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }

            return coordinatesWithAsteroids;
        }

        public IList<Coordinate> GetQuadrant4Asteroids(int orbit)
        {
            var coordinatesWithAsteroids = new List<Coordinate>();
            for (var y = -1; y >= -1 * orbit; y--)
            {
                var x = orbit;
                var coordinateXy = new Coordinate(x, y);
                try
                {
                    var charToTest = GetValueAtXyCoordinate(coordinateXy);
                    if (charToTest == '#')
                        coordinatesWithAsteroids.Add(coordinateXy);
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }

            for (var x = 0; x < orbit; x++)
            {
                var y = -1 * orbit;
                var coordinateXy = new Coordinate(x, y);
                try
                {
                    var charToTest = GetValueAtXyCoordinate(coordinateXy);
                    if (charToTest == '#')
                        coordinatesWithAsteroids.Add(coordinateXy);
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
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
            return new Coordinate(coordinate.X + _coordinateToCheck.X, _coordinateToCheck.Y - coordinate.Y);
        }

        public Coordinate TranslatedCoordinateToXy(Coordinate coordinate)
        {
            return new Coordinate(coordinate.X + _coordinateToCheck.X, Math.Abs(coordinate.Y - _coordinateToCheck.Y));
        }

        public char GetValueAtMapCoordinate(int x, int y)
        {
            return _map[y, x];
        }

        public char GetValueAtXyCoordinate(int x, int y)
        {
            //if (x > XMax() || x < XMin())
            //    throw new IndexOutOfRangeException("x axis");
            //if (y > YMax() || y < YMin())
            //    throw new IndexOutOfRangeException("y axis");

            var xyCoordinate = new Coordinate(x, y);
            return GetValueAtXyCoordinate(xyCoordinate);
        }

        public char GetValueAtXyCoordinate(Coordinate xyCoordinate)
        {
            var mapCoordinate = TranslatedCoordinateToMap(xyCoordinate);
            return _map[mapCoordinate.Y, mapCoordinate.X];
        }
    }
}
