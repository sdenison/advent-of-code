using Aoc.Spaceship.Navigation;
using NUnit.Framework;

namespace Aoc.Spaceship.Unit.Tests.NavigationTests
{
    [TestFixture]
    public class OrbitMapTests
    {
        [Test]
        public void Can_create_satellite_map()
        {
            var mapInput = new List<string>
            {
                "COM)B",
            };
            var map = new OrbitMap(mapInput);
            Assert.IsNotNull(map);
            Assert.AreEqual(1, map.CenterOfMass.Orbits);
        }

        [Test]
        public void Can_create_satellite_map2()
        {
            var mapInput = new List<string>
            {
                "COM)B",
                "B)C"
            };
            var map = new OrbitMap(mapInput);
            Assert.IsNotNull(map);
            Assert.AreEqual(3, map.CenterOfMass.Orbits);
        }

        [Test]
        public void Can_create_satellite_map3()
        {
            var mapInput = new List<string>
            {
                "COM)B",
                "B)G",
                "G)H"
            };
            var map = new OrbitMap(mapInput);
            Assert.IsNotNull(map);
            Assert.AreEqual(6, map.CenterOfMass.Orbits);
        }

        [Test]
        public void Can_get_first_example_orbit_count()
        {
            var mapInput = new List<string>
            {
                "COM)B",
                "B)C",
                "C)D",
                "D)E",
                "E)F",
                "B)G",
                "G)H",
                "D)I",
                "E)J",
                "J)K",
                "K)L"
            };
            var map = new OrbitMap(mapInput);
            Assert.IsNotNull(map);
            Assert.AreEqual(42, map.CenterOfMass.Orbits);
        }
    }
}
