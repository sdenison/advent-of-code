using System.Collections.Generic;

namespace Aoc.Spaceship.Security
{
    public class PasswordGenerator
    {
        public bool Check(int candidate)
        {
            var passesTests = NumberHasDuplicateDigit(candidate);
            passesTests = passesTests && DigitsAlwaysIncrease(candidate);
            return passesTests;
        }

        public bool NumberHasDuplicateDigit(int candidate)
        {
            var candidateString = candidate.ToString();
            var matchingDigits = new List<char>();
            var hasDouble = false;
            foreach (var candidateChar in candidateString.ToCharArray())
            {
                if (matchingDigits.Count > 0)
                {
                    if (matchingDigits[0] == candidateChar)
                    {
                        matchingDigits.Add(candidateChar);
                    }
                    else
                    {
                        if (matchingDigits.Count == 2)
                            hasDouble = true;
                        matchingDigits.Clear();
                        matchingDigits.Add(candidateChar);
                    }
                }
                else
                {
                    matchingDigits.Add(candidateChar);
                }
            }

            if (matchingDigits.Count == 2)
                hasDouble = true;
            return hasDouble;
        }

        public bool DigitsAlwaysIncrease(int candidate)
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

        public List<int> GetCandidatePasswords(int startIndex, int endIndex)
        {
            var candidates = new List<int>();
            for (var candidate = startIndex; candidate <= endIndex; candidate++)
            {
                if (Check(candidate))
                    candidates.Add(candidate);
            }
            return candidates;
        }
    }
}
