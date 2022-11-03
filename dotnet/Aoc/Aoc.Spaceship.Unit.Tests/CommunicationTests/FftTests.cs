using Aoc.Spaceship.Communication;
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
        public void Can_apply_pattern_to_input()
        {
            var input = "15243";
            int[] pattern = { -1, 2, 3 };
            var fft = new Fft(input, pattern);
            //1*1, 5*2, 2*3, 4*1, 3*2
            int[] afterFirstPhase = { 1, 0, 6, 4, 6 };
            fft.PhaseData.Add(afterFirstPhase);
            var sumAfterPhaseStep1 = fft.ApplyPhaseStep(1, fft.PhaseData[0]);
            Assert.AreEqual(7, sumAfterPhaseStep1);
            //New Pattern should be 1, 1, 2, 2, 3, 3
            //When the pattern is applied it should be 1, 2, 2, 3, 3
            //-1*1, 5*2, 2*2, 4*3, 3*3
            int[] afterSecondPhase = { 1, 0, 2, 2, 8 };
            var sumAfterPhaseStep2 = fft.ApplyPhaseStep(2, fft.PhaseData[0]);
            Assert.AreEqual(4, sumAfterPhaseStep2);
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
    }
}
