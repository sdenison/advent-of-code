using System.Collections.Generic;

namespace Aoc.Spaceship.Security
{
    public static class PasswordGenerator
    {
        public static bool ValidatePassword(int candidate)
        {
            return NumberHasDuplicateDigit(candidate) && DigitsAlwaysIncrease(candidate);
        }

        public static List<int> GetCandidatePasswords(int startIndex, int endIndex)
        {
            var candidates = new List<int>();
            for (var candidate = startIndex; candidate <= endIndex; candidate++)
            {
                if (ValidatePassword(candidate))
                    candidates.Add(candidate);
            }
            return candidates;
        }

        private static bool NumberHasDuplicateDigit(int candidate)
        {
            var matchingDigits = new List<char>();
            var hasDouble = false;
            foreach (var candidateChar in candidate.ToString().ToCharArray())
            {
                if (matchingDigits.Count == 0 || matchingDigits[0] == candidateChar)
                    matchingDigits.Add(candidateChar);
                else
                {
                    if (matchingDigits.Count == 2)
                        hasDouble = true;
                    matchingDigits.Clear();
                    matchingDigits.Add(candidateChar);
                }
            }
            if (matchingDigits.Count == 2)
                hasDouble = true;
            return hasDouble;
        }

        private static bool DigitsAlwaysIncrease(int candidate)
        {
            var candidateString = candidate.ToString();
            var biggestNumber = 0;
            foreach (var candidateChar in candidateString.ToCharArray())
            {
                var digit = int.Parse(candidateChar.ToString());
                if (biggestNumber > digit)
                    return false;
                biggestNumber = digit;
            }
            return true;
        }
    }
}
