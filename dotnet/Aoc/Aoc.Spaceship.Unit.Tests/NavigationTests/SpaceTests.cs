using System.Reflection.Metadata.Ecma335;
using Aoc.Spaceship.Navigation;
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
            var space = new Space(emptyMap);
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
            var space = new Space(emptyMap);
            var coordinateToTest = new Coordinate(0, 0);
            var expectedMaxOrbits = 1;
            Assert.AreEqual(expectedMaxOrbits, space.MaxOrbits(coordinateToTest));
        }

        [Test]
        public void Can_get_max_orbits2()
        {
            char[,] emptyMap =
            {
                {'#', '#', '.', '.'},
                {'#', '#', '.', '.'},
                {'#', '#', '.', '.'},
                {'#', '#', '.', '.'},
            };
            var space = new Space(emptyMap);
            var coordinateToTest = new Coordinate(0, 0);
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
        public void Can_get_visible_asteroids_3_by_3()
        {
            var eMap = new[] {new[] {
                '#', '.', '#'
            }};
            char[,] emptyMap = new char[,]
            {
                {'#', '.', '#'},
                {'.', '.', '#'},
                {'#', '#', '#'},
            };
            var space = new Space(emptyMap);
            var coordinateToTest = new Coordinate(0, 0);
            var visibleAsteroids = space.GetVisibleAsteroids(coordinateToTest);

            //Starting from immediately to the right
            coordinateToTest = new Coordinate(1, 0);
            visibleAsteroids = space.GetVisibleAsteroids(coordinateToTest);
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
