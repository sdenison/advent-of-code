def validate_password(candidate: int) -> bool:
    return number_has_duplicate_digit(candidate) and digits_always_increase(candidate)


def get_candidate_passwords(starting_number: int, ending_number: int) -> list[int]:
    candidates: list[int] = []
    for candidate in range(starting_number, ending_number):
        if validate_password(candidate):
            candidates.append(candidate)
    return candidates


def number_has_duplicate_digit(candidate: int) -> bool:
    matching_digits = []
    has_double = False
    for candidate_char in list(str(candidate)):
        if (len(matching_digits) == 0) or (matching_digits[0] == candidate_char):
            matching_digits.append(candidate_char)
        else:
            if len(matching_digits) == 2:
                has_double = True
            matching_digits = [candidate_char]
    if len(matching_digits) == 2:
        has_double = True
    return has_double


def digits_always_increase(candidate: int) -> bool:
    candidate_string = str(candidate)
    biggest_number = 0
    for candidateChar in list(candidate_string):
        digit = int(candidateChar)
        if biggest_number > digit:
            return False
        biggest_number = digit
    return True

