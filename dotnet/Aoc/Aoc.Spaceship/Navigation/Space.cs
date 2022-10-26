using System;
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

        public IList<Coordinate> GetVisibleAsteroids()
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

        public IList<Coordinate> GetVisibleQuadrant3Asteroids()
        {
            var coordinatesWithAsteroids = new List<Coordinate>();
            var maxOrbits = MaxOrbits(_coordinateToCheck);
            for (var orbit = 1; orbit <= maxOrbits; orbit++)
            {
                coordinatesWithAsteroids.AddRange(GetQuadrant3Asteroids(orbit));
            }
            return coordinatesWithAsteroids;
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

            for(var x = 0; x < orbit; x++)
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
            for (var y = 0; y <= orbit; y++)
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

            for (var x = -1; x > -1 * orbit; x--)
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
            for (var y = -1; y > -1 * orbit; y--)
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

            for (var x = -1; x >= -1 * orbit; x--)
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

            //for (var x = 0; x < Math.Max(orbit, XMax()); x++)
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
