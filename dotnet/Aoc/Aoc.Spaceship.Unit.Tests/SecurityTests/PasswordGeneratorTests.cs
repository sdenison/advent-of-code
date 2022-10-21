using Aoc.Spaceship.Security;
using NUnit.Framework;

namespace Aoc.Spaceship.Unit.Tests.SecurityTests
{
    [TestFixture]
    public class PasswordGeneratorTests
    {
        [Test]
        [TestCase(654321, false)]
        [TestCase(135679, false)] //no duplicates
        [TestCase(111111, false)]
        [TestCase(111123, false)]
        [TestCase(112345, true)]
        [TestCase(223450, false)]
        [TestCase(123789, false)]
        public void Can_determine_if_numbers_match_rules(int candidateNumber, bool shouldPassValidation)
        {
            var passedValidation = PasswordGenerator.ValidatePassword(candidateNumber);
            Assert.AreEqual(shouldPassValidation, passedValidation);
        }

        [Test]
        public void Can_get_candidate_passwords()
        {
            var candidates = PasswordGenerator.GetCandidatePasswords(245182, 790572);
            Assert.AreEqual(710, candidates.Count);
        }

        [Test]
        [TestCase(112233, true)]
        [TestCase(123444, false)]
        [TestCase(111122, true)]
        [TestCase(113339, true)]
        public void Numbers_only_repeat_once(int candidateNumber, bool shouldPassValidation)
        {
            var passedValidation = PasswordGenerator.ValidatePassword(candidateNumber);
            Assert.AreEqual(shouldPassValidation, passedValidation);
        }
            
    }
}
