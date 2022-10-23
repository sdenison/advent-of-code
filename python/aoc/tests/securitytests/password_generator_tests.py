import unittest

from spaceship.security.password_generator import *


class TestPasswordGenerator(unittest.TestCase):

    def test_that_we_can_validate_passwords(self):
        candidates = self.get_test_cases()
        for candidate in candidates:
            validated: bool = validate_password(candidate[0])
            self.assertEqual(candidate[1], validated)

    def test_that_we_can_get_puzzle_answer(self):
        candidate_passwords = get_candidate_passwords(245182, 790572)
        self.assertEqual(710, len(candidate_passwords))

    @staticmethod
    def get_test_cases():
        return [
            (654321, False),
            (135679, False),
            (111111, False),
            (111123, False),
            (112345, True),
            (223450, False),
            (123789, False),
            (112233, True),
            (123444, False),
            (111122, True),
            (113339, True)
        ]
