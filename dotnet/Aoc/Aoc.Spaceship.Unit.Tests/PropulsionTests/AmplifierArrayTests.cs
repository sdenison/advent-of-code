using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aoc.Spaceship.Propulsion;
using NUnit.Framework;

namespace Aoc.Spaceship.Unit.Tests.PropulsionTests
{
    [TestFixture]
    public class AmplifierArrayTests
    {
        [Test]
        [TestCase(new[] {4,3,2,1,0}, new[] {3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0}, 43210)]
        [TestCase(new[] {0,1,2,3,4}, new[] {3,23,3,24,1002,24,10,24,1002,23,-1,23, 101,5,23,23,1,24,23,23,4,23,99,0,0}, 54321)]
        [TestCase(new[] {1,0,4,3,2}, new[] {3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0}, 65210)]
        public void Can_get_thrust_for_examples_given(int[] phaseSetting, int[] program, int expectedThrust)
        {
            var amplifierArray = new AmplifierArray(program, phaseSetting);
            var thrust = amplifierArray.GetThrust(5, 0);
            Assert.AreEqual(expectedThrust, thrust);
        }
    }
}
