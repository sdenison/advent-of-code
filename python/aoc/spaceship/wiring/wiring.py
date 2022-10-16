from enum import Enum
from collections import defaultdict


class Direction(Enum):
    right = "R"
    left = "L"
    up = "U"
    down = "D"


class Move:
    @property
    def direction(self) -> Direction:
        return self._direction

    @property
    def distance(self) -> int:
        return self._distance

    def __init__(self, move: str):
        self._direction = Direction(move[0].upper())
        self._distance = int(move[1:])


class Coordinate:
    @property
    def x(self) -> int:
        return self._x

    @property
    def y(self) -> int:
        return self._y

    def __init__(self, x: int, y: int):
        self._x = x
        self._y = y

    def __eq__(self, other):
        return self.x == other.x and self.y == other.y


class Axis(Enum):
    X = 0
    Y = 1


class Step:
    @property
    def position(self) -> Coordinate:
        return self._position

    @property
    def axis(self) -> Axis:
        return self._axis

    @property
    def total_steps_so_far(self) -> int:
        return self._total_steps_so_far

    def __init__(self, position: Coordinate, axis: Axis, total_steps_so_far: int):
        self._position = position
        self._axis = axis
        self._total_steps_so_far = total_steps_so_far


class Intersection:
    @property
    def coordinate(self) -> Coordinate:
        return self._coordinate

    @property
    def steps(self) -> int:
        return self._steps

    def __init__(self, coordinate: Coordinate, steps: int):
        self._coordinate = coordinate
        self._steps = steps


class Wire:
    @property
    def path(self) -> defaultdict[list[Step]]:
        return self._path

    @property
    def current_coordinate(self) -> Coordinate:
        return self._current_coordinate

    def __init__(self, moves: list[str]):
        self._path = defaultdict[Coordinate, list[Step]]()
        self._path = defaultdict(lambda: "Not Present")
        self._current_coordinate = Coordinate(1, 1)
        self.__generate_path(moves)

    def __generate_path(self, moves: list[str]) -> None:
        total_steps_so_far = 0
        for move_str in moves:
            move = Move(move_str)
            total_steps_so_far = self.__make_move(move, total_steps_so_far)

    def __make_move(self, move: Move, total_steps_so_far: int) -> int:
        for i in range(move.distance):
            total_steps_so_far = self.__take_step(move.direction, total_steps_so_far)
        return total_steps_so_far

    def __take_step(self, direction: Direction, total_steps_so_far: int) -> int:
        total_steps_so_far += 1
        match direction:
            case Direction.right:
                self._current_coordinate = Coordinate(self._current_coordinate.x + 1, self._current_coordinate.y)
                self.__add_step_to_path(self._current_coordinate, Step(self._current_coordinate, Axis.X, total_steps_so_far))
            case Direction.left:
                self._current_coordinate = Coordinate(self._current_coordinate.x - 1, self._current_coordinate.y)
                self.__add_step_to_path(self._current_coordinate, Step(self._current_coordinate, Axis.X, total_steps_so_far))
            case Direction.up:
                self._current_coordinate = Coordinate(self._current_coordinate.x, self._current_coordinate.y + 1)
                self.__add_step_to_path(self._current_coordinate, Step(self._current_coordinate, Axis.Y, total_steps_so_far))
            case Direction.down:
                self._current_coordinate = Coordinate(self._current_coordinate.x, self._current_coordinate.y - 1)
                self.__add_step_to_path(self._current_coordinate, Step(self._current_coordinate, Axis.Y, total_steps_so_far))
        return total_steps_so_far

    def __add_step_to_path(self, position: Coordinate, step: Step) -> None:
        if self._path[str(position)] == "Not Present":
            self._path[str(position)] = list[Step]()
        self._path[str(position)].append(step)







