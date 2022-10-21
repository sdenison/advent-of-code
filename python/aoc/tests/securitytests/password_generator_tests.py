import unittest

from spaceship.security.password_generator import *


class TestPasswordGenerator(unittest.TestCase):

    def test_that_we_can_validate_passwords(self):
        candidates = self.get_test_cases()
        for candidate in candidates:
            validated: bool = validate_password(candidate[0])
            self.assertEqual(candidate[1], validated)

    @staticmethod
    def get_test_cases():
        return [
            (654321, False),
            (135679, False)
        ]
