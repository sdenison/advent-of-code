﻿using System.Reflection.Metadata.Ecma335;
using Aoc.Spaceship.Navigation;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;

namespace Aoc.Spaceship.Unit.Tests.NavigationTests
{
    [TestFixture]
    public class SpaceTests
    {
        [Test]
        public void Can_create_space()
        {
            char[,] emptyMap = new char[,]
            {
                {'.', '.'},
                {'.', '.'},
            };
            var space = new Space(emptyMap, new Coordinate(0, 0));
            Assert.IsNotNull(space);
        }

        [Test]
        public void Can_get_max_orbitsl()
        {
            char[,] emptyMap =
            {
                {'#', '#'},
                {'#', '#'},
            };
            var coordinateToTest = new Coordinate(0, 0);
            var space = new Space(emptyMap, coordinateToTest);
            var expectedMaxOrbits = 1;
            Assert.AreEqual(expectedMaxOrbits, space.MaxOrbits(coordinateToTest));
        }

        [Test]
        public void Can_get_max_orbits2()
        {
            char[,] map =
            {
                {'#', '#', '.', '.'},
                {'#', '#', '.', '.'},
                {'#', '#', '.', '.'},
                {'#', '#', '.', '.'},
            };
            var coordinateToTest = new Coordinate(0, 0);
            var space = new Space(map, coordinateToTest);
            var expectedMaxOrbits = 3;
            Assert.AreEqual(expectedMaxOrbits, space.MaxOrbits(coordinateToTest));

            //change coordinates
            coordinateToTest = new Coordinate(2, 1);
            expectedMaxOrbits = 2;
            Assert.AreEqual(expectedMaxOrbits, space.MaxOrbits(coordinateToTest));

            //change coordinates
            coordinateToTest = new Coordinate(2, 3);
            expectedMaxOrbits = 3;
            Assert.AreEqual(expectedMaxOrbits, space.MaxOrbits(coordinateToTest));

            //change coordinates
            coordinateToTest = new Coordinate(3, 2);
            expectedMaxOrbits = 3;
            Assert.AreEqual(expectedMaxOrbits, space.MaxOrbits(coordinateToTest));
        }

        [Test]
        public void Can_get_coordinate_transaction_for_center_at_0_0()
        {
            char[,] map =
            {
                {'#', '#', '.', '.'},
                {'#', '#', '.', '.'},
                {'#', '#', '.', '.'},
                {'#', '#', '.', '.'},
            };
            var proposedCenter = new Coordinate(0, 0);
            var space = new Space(map, proposedCenter);

            //There should be no transactions for 0, 0
            var coordinateToTranslate = new Coordinate(1, 0);
            var expectedCoordinate = new Coordinate(1, 0);
            var translatedCoordinate = space.TranslatedCoordinateToMap(coordinateToTranslate);
            expectedCoordinate.Should().BeEquivalentTo(translatedCoordinate);

            coordinateToTranslate = new Coordinate(3, -2);
            expectedCoordinate = new Coordinate(3, 2);
            translatedCoordinate = space.TranslatedCoordinateToMap(coordinateToTranslate);
            expectedCoordinate.Should().BeEquivalentTo(translatedCoordinate);
        }

        [Test]
        public void Can_translate_coordinates_the_other_way()
        {
            char[,] map =
            {
                {'.', '.', '.', '.'},
                {'.', '.', '.', '.'},
                {'.', '.', '.', 'X'},
                {'.', '.', '.', '.'},
            };
            var proposedCenter = new Coordinate(3, 2);
            var space = new Space(map, proposedCenter);

            //There should be no transactions for 0, 0
            var coordinateToTranslate = new Coordinate(-1, 0);
            var expectedCoordinate = new Coordinate(2, 2);
            var translatedCoordinate = space.TranslatedCoordinateToXy(coordinateToTranslate);
            expectedCoordinate.Should().BeEquivalentTo(translatedCoordinate);

            coordinateToTranslate = new Coordinate(0, -1);
            expectedCoordinate = new Coordinate(3, 3);
            translatedCoordinate = space.TranslatedCoordinateToXy(coordinateToTranslate);
            expectedCoordinate.Should().BeEquivalentTo(translatedCoordinate);
        }

        [Test]
        public void Can_get_translate_coordinates()
        {
            char[,] map = new char[,]
            {
                {'A', 'B', 'C'},
                {'D', 'E', 'F'},
                {'G', 'H', 'I'},
            };
            var coordinateToTest = new Coordinate(0, 2);
            var space = new Space(map, coordinateToTest);
            // (0, 2) map coordinates is the same as (0, 0) x, y coordinates
            Assert.AreEqual('G', space.GetValueAtMapCoordinate(0, 2));
            Assert.AreEqual('G', space.GetValueAtXyCoordinate(0, 0));
            Assert.AreEqual('H', space.GetValueAtXyCoordinate(1, 0));
            Assert.AreEqual('I', space.GetValueAtXyCoordinate(2, 0));
            Assert.AreEqual('D', space.GetValueAtXyCoordinate(0, 1));
            Assert.AreEqual('C', space.GetValueAtXyCoordinate(2, 2));
        }

        [Test]
        public void Can_get_translate_coordinates_on_5_by_5()
        {
            char[,] map = new char[,]
            {
                {'A', 'B', 'C', 'D', 'E'},
                {'F', 'G', 'H', 'I', 'J'},
                {'K', 'L', 'M', 'N', 'O'},
                {'P', 'Q', 'R', 'S', 'T'},
                {'U', 'V', 'W', 'X', 'Y'},
            };
            var coordinateToTest = new Coordinate(3, 2);
            var space = new Space(map, coordinateToTest);
            // (0, 2) map coordinates is the same as (0, 0) x, y coordinates
            Assert.AreEqual('N', space.GetValueAtMapCoordinate(3, 2));
            Assert.AreEqual('N', space.GetValueAtXyCoordinate(0, 0));
            Assert.AreEqual('O', space.GetValueAtXyCoordinate(1, 0));
            Assert.AreEqual('L', space.GetValueAtXyCoordinate(-2, 0));
            Assert.AreEqual('P', space.GetValueAtXyCoordinate(-3, -1));
            Assert.AreEqual('B', space.GetValueAtXyCoordinate(-2, 2));


        }

        [Test]
        public void Can_get_visible_asteroids_from_quadrent_1()
        {
            char[,] map = new char[,]
            {
                {'#', '#', '#'},
                {'.', '.', '#'},
                {'#', '#', '#'},
            };
            var coordinateToTest = new Coordinate(0, 2);
            var space = new Space(map, coordinateToTest);
            var visibleAsteroids = space.GetVisibleAsteroids();
            Assert.AreEqual(6, visibleAsteroids.Count);
        }

        [Test]
        public void Can_get_visible_asteroids_from_quadrent_1_5_by_5()
        {
            char[,] map = new char[,]
            {
                {'#', '#', '#', '.', '#'},
                {'.', '.', '#', '.', '#'},
                {'.', '#', '#', '.', '#'},
                {'.', '.', '#', '.', '#'},
                {'#', '.', '#', '.', '#'},
            };
            var coordinateToTest = new Coordinate(0, 4);
            var space = new Space(map, coordinateToTest);
            var visibleAsteroids = space.GetVisibleAsteroids();
            Assert.AreEqual(13, visibleAsteroids.Count);
        }

        [Test]
        public void Can_get_visible_asteroids_from_quadrent_2()
        {
            char[,] map = new char[,]
            {
                {'#', '#', '#'},
                {'.', '.', '#'},
                {'#', '#', '#'},
            };
            var coordinateToTest = new Coordinate(2, 2);
            var space = new Space(map, coordinateToTest);
            var visibleAsteroids = space.GetVisibleAsteroids();
            Assert.AreEqual(6, visibleAsteroids.Count);
        }

        [Test]
        public void Can_get_visible_asteroids_from_quadrent_1_and_two_combined()
        {
            char[,] map = new char[,]
            {
                {'#', '#', '#', '.', '#'},
                {'.', '.', '#', '.', '#'},
                {'.', '#', '#', '.', '#'},
                {'.', '.', '#', '.', '#'},
                {'#', '.', '#', '.', '#'},
            };
            var coordinateToTest = new Coordinate(1, 4);
            var space = new Space(map, coordinateToTest);
            var visibleAsteroids = space.GetVisibleAsteroids();
            Assert.AreEqual(14, visibleAsteroids.Count);
        }

        [Test]
        public void Can_get_visible_asteroids_from_quadrent_2_2()
        {
            char[,] map = new char[,]
            {
                {'#', '#', '#', '.', '#'},
                {'.', '.', '#', '.', '#'},
                {'.', '#', '#', '.', '#'},
                {'.', '.', '#', '.', 'B'},
                {'#', '.', '#', '.', 'A'},
            };
            var coordinateToTest = new Coordinate(4, 4);
            var space = new Space(map, coordinateToTest);
            Assert.AreEqual('A', space.GetValueAtXyCoordinate(0, 0));
            Assert.AreEqual('B', space.GetValueAtXyCoordinate(0, 1));
            var visibleAsteroids = space.GetVisibleAsteroids();
            Assert.AreEqual(12, visibleAsteroids.Count);
        }

        [Test]
        public void Can_get_visible_asteroids_from_quadrent_3()
        {
            char[,] map = new char[,]
            {
                {'#', '#', '#', '.', 'A'},
                {'.', '.', '#', '.', 'B'},
                {'.', '#', '#', '.', '#'},
                {'.', '.', '#', '.', '#'},
                {'#', '.', '#', '.', '#'},
            };
            var coordinateToTest = new Coordinate(4, 0);
            var space = new Space(map, coordinateToTest);
            //Assert.AreEqual('A', space.GetValueAtXyCoordinate(0, 0));
            Assert.AreEqual('B', space.GetValueAtXyCoordinate(0, -1));
            //var x = space.GetValueAtXyCoordinate(0, 1);
            //var visibleAsteroids = space.GetVisibleAsteroids();
            var visibleAsteroids = space.GetVisibleQuadrant3Asteroids();
            Assert.AreEqual(9, visibleAsteroids.Count);
        }

        [Test]
        public void Can_get_visible_asteroids_from_quadrent_4()
        {
            char[,] map = new char[,]
            {
                {'A', '#', '#', '.', '#'},
                {'B', '.', '#', '.', '#'},
                {'#', '#', '.', '.', '#'},
                {'.', '.', '.', '.', '#'},
                {'C', '#', '#', '.', '#'},
            };
            var coordinateToTest = new Coordinate(0, 0);
            var space = new Space(map, coordinateToTest);

            //There should be no (1, 1)
            //Assert.AreEqual('A', space.GetValueAtXyCoordinate(1, 1));

            Assert.AreEqual('A', space.GetValueAtXyCoordinate(0, 0));
            Assert.AreEqual('B', space.GetValueAtXyCoordinate(0, -1));
            Assert.AreEqual('C', space.GetValueAtXyCoordinate(0, -4));
            var visibleAsteroids = space.GetVisibleAsteroids();
            Assert.AreEqual(12, visibleAsteroids.Count);
        }

        [Test]
        public void Can_get_visible_asteroids_from_quadrent_all()
        {
            char[,] map = new char[,]
            {
                {'#', '#', '#', '.', '#'},
                {'.', '.', '#', '.', '#'},
                {'#', '#', 'A', '.', '#'},
                {'.', '.', 'B', '.', '#'},
                {'#', '#', '#', '.', '#'},
            };
            var coordinateToTest = new Coordinate(2, 2);
            var space = new Space(map, coordinateToTest);
            Assert.AreEqual('A', space.GetValueAtXyCoordinate(0, 0));
            Assert.AreEqual('B', space.GetValueAtXyCoordinate(0, -1));
            var visibleAsteroids = space.GetVisibleAsteroids();
            Assert.AreEqual(14, visibleAsteroids.Count);
        }

        [Test]
        public void Can_make_sure_x_y_coordinates_translate()
        {
            char[,] map = new char[,]
            {
                {'A', '#', '#', '.', '#'},
                {'B', 'X', '#', '.', '#'},
                {'#', '#', '.', '.', '#'},
                {'.', '.', '.', '.', '#'},
                {'C', '#', '#', '.', '#'},
            };
            var coordinateToTest = new Coordinate(1, 1);
            var space = new Space(map, coordinateToTest);

            Assert.AreEqual(3, space.XMax());
            Assert.AreEqual(-1, space.XMin());
            Assert.AreEqual(1, space.YMax());
            Assert.AreEqual(-3, space.YMin());
        }
    }
}