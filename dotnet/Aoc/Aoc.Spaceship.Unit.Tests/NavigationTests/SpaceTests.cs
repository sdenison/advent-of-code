using System.Reflection.Metadata.Ecma335;
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

            coordinateToTranslate = new Coordinate(3, 2);
            expectedCoordinate = new Coordinate(3, 2);
            translatedCoordinate = space.TranslatedCoordinateToMap(coordinateToTranslate);
            expectedCoordinate.Should().BeEquivalentTo(translatedCoordinate);
        }

        [Test]
        public void Can_get_coordinate_transaction_for_center_at_3_2()
        {
            char[,] map =
            {
                {'#', '#', '.', '.'},
                {'#', '#', '.', '.'},
                {'#', '#', '.', '.'},
                {'#', '#', '.', '.'},
            };
            var proposedCenter = new Coordinate(3, 2);
            var space = new Space(map, proposedCenter);

            //There should be no transactions for 0, 0
            var coordinateToTranslate = new Coordinate(2, 2);
            var expectedCoordinate = new Coordinate(-1, 0);
            var translatedCoordinate = space.TranslatedCoordinateToMap(coordinateToTranslate);
            expectedCoordinate.Should().BeEquivalentTo(translatedCoordinate);

            coordinateToTranslate = new Coordinate(3, 3);
            expectedCoordinate = new Coordinate(0, 1);
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

            coordinateToTranslate = new Coordinate(0, 1);
            expectedCoordinate = new Coordinate(3, 3);
            translatedCoordinate = space.TranslatedCoordinateToXy(coordinateToTranslate);
            expectedCoordinate.Should().BeEquivalentTo(translatedCoordinate);
        }

        [Test]
        public void Can_get_visible_asteroids_3_by_3()
        {
            var eMap = new[] {
                new[] { 'a', 'b', 'c' },
                new[] { 'd', 'e', 'f' },
                new[] { 'g', 'h', 'i' }
            };
            char[,] emptyMap = new char[,]
            {
                {'A', 'B', 'C'},
                {'D', 'E', 'F'},
                {'G', 'H', 'I'},
            };
            Assert.AreEqual('C', emptyMap[0, 2]);
            Assert.AreEqual('G', emptyMap[2, 0]);

            Assert.AreEqual('c', eMap[0][2]);
            var coordinateToTest = new Coordinate(0, 2);

            var space = new Space(emptyMap, coordinateToTest);
            Assert.AreEqual('G', space.GetCoordinate(0, 2));

            var xyCoordinates = new Coordinate(1, 0);
            var expectedMapCoordinate = new Coordinate(1, 2);
            var mapCoordinate = space.TranslatedCoordinateToMap(xyCoordinates);
            //expectedMapCoordinate.Should().BeEquivalentTo(mapCoordinate);

            //xyCoordinates = new Coordinate(0, 1);
            //expectedMapCoordinate = new Coordinate(0, 1);
            //mapCoordinate = space.TranslatedCoordinateToMap(xyCoordinates);
            //expectedMapCoordinate.Should().BeEquivalentTo(mapCoordinate);


            //var visibleAsteroids = space.GetVisibleAsteroids();
            //Assert.AreEqual(4, visibleAsteroids.Count);

            ////Starting from immediately to the right
            //coordinateToTest = new Coordinate(1, 0);
            //visibleAsteroids = space.GetVisibleAsteroids();
        }

        //[Test]
        //public void Can_get_visible_asteroids_3_by_3()
        //{
        //    char[,] emptyMap = new char[,]
        //    {
        //        {'#', '.', '#'},
        //        {'.', '.', '#'},
        //        {'#', '#', '#'},
        //    };
        //    var space = new Space(emptyMap);
        //    var coordinateToTest = new Coordinate(0, 0);
        //    var visibleAsteroids = space.GetVisibleAsteroids(coordinateToTest);
        //}
    }
}
