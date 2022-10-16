import sys
import unittest

from spaceship.wiring.wiring import *


class TestWiringUnit(unittest.TestCase):

    def test_that_direction_can_be_parsed(self):
        move_string = "U533"
        move = Move(move_string)
        self.assertEqual(Direction.up, move.direction)
        self.assertEqual(533, move.distance)

    def test_that_we_can_create_a_wire(self):
        moves = ["R2"]
        wire = Wire(moves)
        assert wire is not None

    def test_move_up(self):
        moves = ["U5"]
        wire = Wire(moves)
        expected_coordinate = Coordinate(1, 6)
        self.assertEqual(expected_coordinate, wire.current_coordinate)

    def test_can_move_down(self):
        moves = ["D3"]
        wire = Wire(moves)
        expected_coordinate = Coordinate(1, -2)
        self.assertEqual(wire.current_coordinate, expected_coordinate)

    def test_can_get_grid_location_after_several_moves(self):
        moves = ["R2", "U5", "L3", "D1"]
        wire = Wire(moves)
        expected_coordinate = Coordinate(0, 5)
        self.assertEqual(wire.current_coordinate, expected_coordinate)

    def test_can_figure_out_where_two_wires_cross(self):
        green_wire = Wire(["R8", "U5", "L5", "D3"])
        red_wire = Wire(["U7", "R6", "D4", "L4"])
        grid = Grid(green_wire, red_wire)
        self.assertEqual(2, len(grid.get_intersections()))
        expected_first_crossing = Coordinate(7, 6)
        self.assertEqual(grid.get_intersections()[0].coordinate, expected_first_crossing)
        expected_second_crossing = Coordinate(4, 4)
        self.assertEqual(grid.get_intersections()[1].coordinate, expected_second_crossing)
        port = Coordinate(1, 1)
        expected_manhattan_distance = 6
        manhattan_distance = Grid.get_manhattan_distance(port, grid.get_intersections()[1].coordinate)
        self.assertEqual(expected_manhattan_distance, manhattan_distance)

    def test_can_get_manhattan_distance(self):
        coordinate_a = Coordinate(0, 0)
        coordinate_b = Coordinate(5, 7)
        expected_manhattan_distance = 12
        manhattan_distance = Grid.get_manhattan_distance(coordinate_a, coordinate_b)
        self.assertEqual(expected_manhattan_distance, manhattan_distance)

    def test_can_get_manhattan_distance_for_examples_given_2(self):
        moves_a = ["R75", "D30", "R83", "U83", "L12", "D49", "R71", "U7", "L72"]
        moves_b = ["U62", "R66", "U55", "R34", "D71", "R55", "D58", "R83"]
        wire_a = Wire(moves_a)
        wire_b = Wire(moves_b)
        grid = Grid(wire_a, wire_b)
        port = Coordinate(1, 1)
        lowest_manhattan = sys.maxsize
        for intersection in grid.get_intersections():
            manhattan = Grid.get_manhattan_distance(port, intersection.coordinate)
            if manhattan < lowest_manhattan:
                lowest_manhattan = manhattan
        self.assertEqual(159, lowest_manhattan)




