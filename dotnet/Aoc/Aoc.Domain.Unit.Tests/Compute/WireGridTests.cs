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
        public void Can_figure_out_where_two_wires_cross()
        {
            var wireA = new Wire(new[] {"R8", "U5", "L5", "D3"});
            var wireB = new Wire(new[] {"U7", "R6", "D4", "L4"});
            var grid = new Grid(wireA, wireB);
            Assert.AreEqual(2, grid.Intersections.Count);
            var expectedFirstCrossing = new Coordinate(7, 6);
            grid.Intersections[0].Should().BeEquivalentTo(expectedFirstCrossing);
            var expectedSecondCrossing = new Coordinate(4, 4);
            grid.Intersections[1].Should().BeEquivalentTo(expectedSecondCrossing);
            var port = new Coordinate(1, 1);
            var expectedManhattanDistance = 6;
            var manhattanDistance = Grid.GetManhattanDistance(port, grid.Intersections[1]);
            Assert.AreEqual(expectedManhattanDistance, manhattanDistance);
        }

        [Test]
        public void Can_get_manhattan_distance()
        {
            var coordinateA = new Coordinate(0, 0);
            var coordinateB = new Coordinate(5, 7);
            var expectedManhattanDistance = 12;
            var manhattanDistance = Grid.GetManhattanDistance(coordinateA, coordinateB);
            Assert.AreEqual(expectedManhattanDistance, manhattanDistance);
        }

        [Test]
        public void Can_get_manhattan_distance_for_examples_given_2()
        {
            var movesA = new[] { "R75", "D30", "R83", "U83", "L12", "D49", "R71", "U7", "L72" };
            var movesB = new[] { "U62", "R66", "U55", "R34", "D71", "R55", "D58", "R83" };
            var wireA = new Wire(movesA);
            var wireB = new Wire(movesB);
            var grid = new Grid(wireA, wireB);
            var port = new Coordinate(1, 1);
            var lowestManhattan = int.MaxValue;
            foreach (var intersection in grid.Intersections)
            {
                var manhattan = Grid.GetManhattanDistance(port, intersection);
                if (manhattan < lowestManhattan)
                    lowestManhattan = manhattan;
            }
            Assert.AreEqual(159, lowestManhattan);
        }

        [Test]
        public void Can_get_manhattan_distance_for_examples_given_3()
        {
            var movesA = new[] { "R98", "U47", "R26", "D63", "R33", "U87", "L62", "D20", "R33", "U53", "R51"};
            var movesB = new[] { "U98","R91","D20","R16","D67","R40","U7","R15","U6","R7" };
            var wireA = new Wire(movesA);
            var wireB = new Wire(movesB);
            var grid = new Grid(wireA, wireB);
            var port = new Coordinate(1, 1);
            var lowestManhattan = int.MaxValue;
            foreach (var intersection in grid.Intersections)
            {
                var manhattan = Grid.GetManhattanDistance(port, intersection);
                if (manhattan < lowestManhattan)
                    lowestManhattan = manhattan;
            }
            Assert.AreEqual(135, lowestManhattan);
        }
    }
}
