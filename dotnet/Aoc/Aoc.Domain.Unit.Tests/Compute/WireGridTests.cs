using Aoc.Domain.Compute;
using FluentAssertions;
using NUnit.Framework;

namespace Aoc.Domain.Unit.Tests.Compute
{
    [TestFixture]
    public class WireGridTests
    {

        [Test]
        public void Can_create_wire_grid()
        {
            var wireGrid = new WireGrid(new List<string>{"R34"});
        }

        [Test]
        public void Can_parse_move()
        {
            var move = new Move("R75");
            Assert.AreEqual("R", move.Direction);
            Assert.AreEqual(75, move.Distance);

            move = new Move("U323");
            Assert.AreEqual("U", move.Direction);
            Assert.AreEqual(323, move.Distance);
        }

        [Test]
        public void Can_get_grid_location_after_moves()
        {
            var grid = new WireGrid(new List<string> {"R2"});
            var expectedCoordinate = new Coordinate(3, 1);
            grid.CoordinateAfterMove.Should().BeEquivalentTo(expectedCoordinate);
        }

        [Test]
        public void Can_get_grid_location_after_several_moves()
        {
            var grid = new WireGrid(new List<string> {"R2", "U5", "L3", "D1"});
            var expectedCoordinate = new Coordinate(0, 5);
            grid.CoordinateAfterMove.Should().BeEquivalentTo(expectedCoordinate);
        }

        //[Test]
        //public void Can_get_manhattan_distance_for_examples_given()
        //{
        //    var moves = new List<string>
        //    {
        //        "R75", "D30","R83","U83","L12","D49","R71","U7","L72",
        //        "U62","R66","U55","R34","D71","R55","D58","R83"
        //    };
        //    var grid = new WireGrid(moves);
        //    Assert.AreEqual(159, grid.ManhattanDistance);
        //}

    }
}
