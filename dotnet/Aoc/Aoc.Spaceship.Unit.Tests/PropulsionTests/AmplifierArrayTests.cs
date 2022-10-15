using Aoc.Spaceship.Propulsion;
using NUnit.Framework;

namespace Aoc.Spaceship.Unit.Tests.PropulsionTests
{
    [TestFixture]
    public class AmplifierArrayTests
    {
        [Test]
        [TestCase(new[] { 4, 3, 2, 1, 0 }, new[] { 3, 15, 3, 16, 1002, 16, 10, 16, 1, 16, 15, 15, 4, 15, 99, 0, 0 }, 43210)]
        [TestCase(new[] { 0, 1, 2, 3, 4 }, new[] { 3, 23, 3, 24, 1002, 24, 10, 24, 1002, 23, -1, 23, 101, 5, 23, 23, 1, 24, 23, 23, 4, 23, 99, 0, 0 }, 54321)]
        [TestCase(new[] { 1, 0, 4, 3, 2 }, new[] { 3, 31, 3, 32, 1002, 32, 10, 32, 1001, 31, -2, 31, 1007, 31, 0, 33, 1002, 33, 7, 33, 1, 33, 31, 31, 1, 32, 31, 31, 4, 31, 99, 0, 0, 0 }, 65210)]
        public async Task Can_get_thrust_for_examples_given(int[] phaseSetting, int[] program, int expectedThrust)
        {
            var amplifierArray = new AmplifierArray(program);
            var thrust = amplifierArray.GetMaximumThrustWithFeedbackLoop(phaseSetting);
            Assert.AreEqual(expectedThrust, thrust);
        }

        [Test]
        public void Can_get_day_7_part_1_answer()
        {
            var program = GetDay7InputProgram();
            var amplifierArray = new AmplifierArray(program);
            var maximumThrust = amplifierArray.GetMaximumThrust(new[] { 0, 1, 2, 3, 4 });
            Assert.AreEqual(92663, maximumThrust);
        }

        [Test]
        public void Can_get_amplifier_to_promt_for_input()
        {
        }

        [Test, Ignore("Takes too long to run")]
        public void Can_get_feedback_answer_from_example()
        {
            var program =
            new[]{
                3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,
                27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5
            };
            var amplifierArray = new AmplifierArray(program);
            var maximumThrust = amplifierArray.GetMaximumThrust(new[] {9, 8, 7, 6, 5});
            Assert.AreEqual(139629729, maximumThrust);
        }

        [Test, Ignore("Takes too long to run")]
        public void Can_get_feedback_answer_from_example2()
        {
            var program =
            new[]{
                3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,
                -5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,
                53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10
            };
            var amplifierArray = new AmplifierArray(program);
            var maximumThrust = amplifierArray.GetMaximumThrust(new[] { 5, 6, 7, 8, 9 });
            Assert.AreEqual(18216, maximumThrust);
        }



        [Test, Ignore("Takes too long to run")]
        public void Can_get_day_7_part_2_answer()
        {
            var program = GetDay7InputProgram();
            var amplifierArray = new AmplifierArray(program);
            var maximumThrust = amplifierArray.GetMaximumThrust(new[] { 5, 6, 7, 8, 9 });
            Assert.AreEqual(14365052, maximumThrust);
        }

        public int[] GetDay7InputProgram()
        {
            return new[]
            {
                3, 8, 1001, 8, 10, 8, 105, 1, 0, 0, 21, 34, 47, 72, 81, 102, 183, 264, 345, 426, 99999, 3, 9, 102, 5, 9,
                9, 1001, 9, 3, 9, 4, 9, 99, 3, 9, 101, 4, 9, 9, 1002, 9, 3, 9, 4, 9, 99, 3, 9, 102, 3, 9, 9, 101, 2, 9,
                9, 102, 5, 9, 9, 1001, 9, 3, 9, 1002, 9, 4, 9, 4, 9, 99, 3, 9, 101, 5, 9, 9, 4, 9, 99, 3, 9, 101, 3, 9,
                9, 1002, 9, 5, 9, 101, 4, 9, 9, 102, 2, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9,
                4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2,
                9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1,
                9, 9, 4, 9, 99, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9,
                102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9,
                1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 101, 1, 9, 9, 4, 9,
                3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4,
                9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 102, 2, 9, 9,
                4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 99, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9,
                1, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001,
                9, 1, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 99, 3, 9,
                102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9,
                1001, 9, 1, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3,
                9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 99
            };
        }
    }
}
