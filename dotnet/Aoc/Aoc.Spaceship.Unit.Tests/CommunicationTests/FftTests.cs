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
        public void Can_get_offset_with_just_math()
        {
            var input = "15243";
            int[] pattern = { 1, 2, 3 };
            var fft = new Fft(input, pattern);

            var expectedTwoPattern = new[] { 1, 1, 2, 2, 3, 3 };
            Assert.AreEqual(1, fft.GetPatternInt(2, 0)); //0
            Assert.AreEqual(2, fft.GetPatternInt(2, 1)); //0
            Assert.AreEqual(2, fft.GetPatternInt(2, 2)); //1
            Assert.AreEqual(3, fft.GetPatternInt(2, 3)); //1
            Assert.AreEqual(3, fft.GetPatternInt(2, 4)); //2
            Assert.AreEqual(1, fft.GetPatternInt(2, 5)); //2
            Assert.AreEqual(1, fft.GetPatternInt(2, 6)); //1
            Assert.AreEqual(2, fft.GetPatternInt(2, 7)); //1
            Assert.AreEqual(2, fft.GetPatternInt(2, 8)); //1
        }

        [Test]
        public void Can_apply_phase_1()
        {
            var input = "12345678";
            int[] pattern = { 0, 1, 0, -1 };
            var fft = new Fft(input, pattern);
            foreach (var VARIABLE in fft.ApplyPhases(1))
            {
            }

            fft.ApplyPhases(1);

            int[] afterOnePhase = {4, 8, 2, 2, 6, 1, 5, 8};
            Assert.AreEqual(afterOnePhase, fft.PhaseData[1]);
        }

        [Test]
        public void Can_apply_phase_4()
        {
            var input = "12345678";
            int[] pattern = { 0, 1, 0, -1 };
            var fft = new Fft(input, pattern);
            foreach (var applyPhase in fft.ApplyPhases(4))
            {
                
            }
            int[] afterOnePhase = { 4, 8, 2, 2, 6, 1, 5, 8 };
            Assert.AreEqual(afterOnePhase, fft.PhaseData[1]);
            int[] afterTwoPhases = { 3, 4, 0, 4, 0, 4, 3, 8 };
            Assert.AreEqual(afterTwoPhases, fft.PhaseData[2]);
            int[] afterThreePhases = { 0, 3, 4, 1, 5, 5, 1, 8 };
            Assert.AreEqual(afterThreePhases, fft.PhaseData[3]);
            int[] afterFourPhases = { 0, 1, 0, 2, 9, 4, 9, 8 };
            Assert.AreEqual(afterFourPhases, fft.PhaseData[4]);
        }

        [Test]
        public void Can_apply_100_phases_from_example()
        {
            var input = "80871224585914546619083218645595";
            int[] pattern = { 0, 1, 0, -1 };
            var fft = new Fft(input, pattern);
            foreach (var applyPhase in fft.ApplyPhases(100))
            {

            }
            int[] expectedOutput = { 2, 4, 1, 7, 6, 1, 7, 6 };
            Assert.AreEqual(expectedOutput, fft.PhaseData[100].ToArray().SubArray(0, 8));
        }

        [Test] //Takes 15 seconds
        public void Can_get_day_16_step_1_answer()
        {
            var input = GetDay16PuzzleData();
            int[] pattern = { 0, 1, 0, -1 };
            var fft = new Fft(input, pattern);

            var path = "D:\\temp\\day16_step_1.txt";
            if (File.Exists(path))
                File.Delete(path);
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(input);
            }
            foreach (var applyPhase in fft.ApplyPhases(100))
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(applyPhase);
                }
            }
            var puzzleAnswer = fft.PhaseData[100].ToArray().SubArray(0, 8);
            int[] expectedOutput = { 4, 9, 2, 5, 4, 7, 7, 9 };
            var lines = fft.PhaseData.ToStringList();
            Assert.AreEqual(expectedOutput, puzzleAnswer);
        }

        [Test, Ignore("too slow")]
        //[Test]
        public void Can_get_message_offset() 
        {
            var input = "03036732577212944063491565474664";
            var finalInput = "";
            for (var i = 0; i < 10000; i++)
                finalInput += input;
            int[] pattern = { 0, 1, 0, -1 };
            var fft = new Fft(finalInput, pattern);
            foreach (var applyPhase in fft.ApplyPhases(1))
            {
            }
        }

        public static string GetDay16PuzzleData()
        {
            return
                "59793513516782374825915243993822865203688298721919339628274587775705006728427921751430533510981343323758576985437451867752936052153192753660463974146842169169504066730474876587016668826124639010922391218906707376662919204980583671961374243713362170277231101686574078221791965458164785925384486127508173239563372833776841606271237694768938831709136453354321708319835083666223956618272981294631469954624760620412170069396383335680428214399523030064601263676270903213996956414287336234682903859823675958155009987384202594409175930384736760416642456784909043049471828143167853096088824339425988907292558707480725410676823614387254696304038713756368483311";
        }
    }
}
