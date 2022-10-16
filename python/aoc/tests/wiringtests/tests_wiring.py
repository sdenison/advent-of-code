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

