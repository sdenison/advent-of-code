from enum import Enum


class Opcodes(Enum):
    ADD = 1
    MULTIPLY = 2
    PUT = 3
    DISPLAY = 4
    JUMPIFTRUE = 5
    JUMPIFFALSE = 6
    LESSTHAN = 7
    EQUALS = 8
    HALT = 99
