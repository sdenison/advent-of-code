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
        public void Can_get_thrust()
        {
            var phaseSetting = new[] {4, 3, 2, 1, 0};
            var program = new[] {3, 15, 3, 16, 1002, 16, 10, 16, 1, 16, 15, 15, 4, 15, 99, 0, 0};
            var amplifierArray = new AmplifierArray(program, phaseSetting);
            var expectedThrust = 43210;
            var thrust = amplifierArray.GetThrust(5, 0);
            Assert.AreEqual(expectedThrust, thrust);
        }
    }
}
