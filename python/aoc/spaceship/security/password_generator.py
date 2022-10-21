def validate_password(candidate: int) -> bool:
    return number_has_duplicate_digit(candidate)


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
