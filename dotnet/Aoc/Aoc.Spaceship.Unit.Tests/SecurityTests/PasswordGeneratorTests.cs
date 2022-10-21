using Aoc.Spaceship.Security;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;

namespace Aoc.Spaceship.Unit.Tests.SecurityTests
{
    [TestFixture]
    public class PasswordGeneratorTests
    {
        [Test]
        public void Can_create_password_generator()
        {
            var passwordGenerator = new PasswordGenerator();
        }

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
            var passwordGenerator = new PasswordGenerator();
            var passedValidation = passwordGenerator.Check(candidateNumber);
            Assert.AreEqual(shouldPassValidation, passedValidation);
        }

        [Test]
        public void Can_get_candidate_passwords()
        {
            var passwordGenerator = new PasswordGenerator();
            var candidates = passwordGenerator.GetCandidatePasswords(245182, 790572);
            Assert.AreEqual(710, candidates.Count);
        }

        [Test]
        [TestCase(112233, true)]
        [TestCase(123444, false)]
        [TestCase(111122, true)]
        [TestCase(113339, true)]
        public void Numbers_only_repeat_once(int candidateNumber, bool shouldPassValidation)
        {
            var passwordGenerator = new PasswordGenerator();
            var passedValidation = passwordGenerator.Check(candidateNumber);
            Assert.AreEqual(shouldPassValidation, passedValidation);
        }
            
    }
}
