using Aoc.Domain.Compute;
using FluentAssertions;
using NUnit.Framework;

namespace Aoc.Domain.Unit.Tests.Compute
{
    [TestFixture]
    public class WireGridTests
    {
        [Test]
        public void Can_create_wire()
        {
            var moves = Move.ParseMoveList(new[] {"R2"});
            var wire = new Wire(moves);
            Assert.IsNotNull(wire);
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
        public void Can_move_up()
        {
            var moves = Move.ParseMoveList(new[] {"U5"});
            var wire = new Wire(moves);
            var expectedCoordinate = new Coordinate(1, 6);
            wire.CurrentCoordinate.Should().BeEquivalentTo(expectedCoordinate);
        }

        [Test]
        public void Can_move_down()
        {
            var moves = Move.ParseMoveList(new[] {"D3"});
            var wire = new Wire(moves);
            var expectedCoordinate = new Coordinate(1, -2);
            wire.CurrentCoordinate.Should().BeEquivalentTo(expectedCoordinate);
        }

        [Test]
        public void Can_get_grid_location_after_several_moves()
        {
            var moves = new[] { "R2", "U5", "L3", "D1" };
            var wire = new Wire(moves);
            var expectedCoordinate = new Coordinate(0, 5);
            wire.CurrentCoordinate.Should().BeEquivalentTo(expectedCoordinate);
        }

        [Test]
        public void Can_create_a_path_for_a_wire()
        {
            var moves = new[] {"R2"};
            var wire = new Wire(moves);
            Assert.AreEqual(3, wire.Path.Count);
            wire.Path[0].Should().BeEquivalentTo(new Coordinate(1, 1));
            wire.Path[1].Should().BeEquivalentTo(new Coordinate(2, 1));
        }

        [Test]
        public void Can_figure_out_where_two_wires_cross()
        {

        }


        //[Test]
        //public void Can_get_manhattan_distance_for_examples_given()
        //{
        //    var moves = new List<string>
        //    {
        //        "R75", "D30","R83","U83","L12","D49","R71","U7","L72",
        //        "U62","R66","U55","R34","D71","R55","D58","R83"
        //    };
        //    var grid = new Grid(moves);
        //    Assert.AreEqual(159, grid.ManhattanDistance);
        //}

    }
}
