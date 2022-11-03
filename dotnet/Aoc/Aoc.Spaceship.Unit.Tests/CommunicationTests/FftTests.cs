using Aoc.Spaceship.Communication;
using Aoc.Spaceship.Utilities;
using NUnit.Framework;


namespace Aoc.Spaceship.Unit.Tests.CommunicationTests
{
    [TestFixture]
    public class FftTests
    {
        [Test]
        public void Can_work_with_phases()
        {
            var input = "15243";
            int[] pattern = { 1, 2, 3 };
            var fft = new Fft(input, pattern);
            //The first item in our phase data should be 1
            Assert.AreEqual(1, fft.PhaseData[0][0]);
        }

        [Test]
        public void Can_work_with_patterns()
        {
            var input = "15243";
            int[] pattern = { 1, 2, 3 };
            var fft = new Fft(input, pattern);
            var expectedOnePattern = new[] { 1, 2, 3 };
            Assert.AreEqual(expectedOnePattern, fft.GetPattern(1));
            var expectedTwoPattern = new[] { 1, 1, 2, 2, 3, 3 };
            Assert.AreEqual(expectedTwoPattern, fft.GetPattern(2));
        }

        [Test]
        public void Can_apply_phase_1()
        {
            var input = "12345678";
            int[] pattern = { 0, 1, 0, -1 };
            var fft = new Fft(input, pattern);
            fft.ApplyPhase(1);
            int[] afterOnePhase = {4, 8, 2, 2, 6, 1, 5, 8};
            Assert.AreEqual(afterOnePhase, fft.PhaseData[1]);
        }

        [Test]
        public void Can_apply_phase_4()
        {
            var input = "12345678";
            int[] pattern = { 0, 1, 0, -1 };
            var fft = new Fft(input, pattern);
            fft.ApplyPhases(4);
            int[] afterOnePhase = {4, 8, 2, 2, 6, 1, 5, 8};
            Assert.AreEqual(afterOnePhase, fft.PhaseData[1]);
            int[] afterTwoPhases= { 3, 4, 0, 4, 0, 4, 3, 8 };
            Assert.AreEqual(afterTwoPhases, fft.PhaseData[2]);
            int[] afterThreePhases= { 0, 3, 4, 1, 5, 5, 1, 8};
            Assert.AreEqual(afterThreePhases, fft.PhaseData[3]);
            int[] afterFourPhases = {0, 1, 0, 2, 9, 4, 9, 8};
            Assert.AreEqual(afterFourPhases, fft.PhaseData[4]);
        }

        [Test]
        public void Can_apply_100_phases_from_example()
        {
            var input = "80871224585914546619083218645595";
            int[] pattern = { 0, 1, 0, -1 };
            var fft = new Fft(input, pattern);
            fft.ApplyPhases(100);
            int[] expectedOutput = {2, 4, 1, 7, 6, 1, 7, 6};
            var subArray = fft.PhaseData[2].ToArray<int>();
            Assert.AreEqual(expectedOutput, fft.PhaseData[100].ToArray().SubArray(0, 8));
        }

        [Test]
        public void Can_apply_100_phases_from_example2()
        {
            var input = "19617804207202209144916044189917";
            int[] pattern = { 0, 1, 0, -1 };
            var fft = new Fft(input, pattern);
            fft.ApplyPhases(100);
            int[] expectedOutput = {7, 3, 7, 4, 5, 4, 1, 8};
            var subArray = fft.PhaseData[2].ToArray<int>();
            Assert.AreEqual(expectedOutput, fft.PhaseData[100].ToArray().SubArray(0, 8));
        }
    }
}
