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


