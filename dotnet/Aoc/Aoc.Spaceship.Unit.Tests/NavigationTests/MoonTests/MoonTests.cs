using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aoc.Spaceship.Navigation.Moons;
using NUnit.Framework;

namespace Aoc.Spaceship.Unit.Tests.NavigationTests.MoonTests
{
    [TestFixture]
    public class MoonTests
    {
        [Test]
        public void Can_create_coordinate_from_string()
        {
            var coordinateString = "<x=-1, y=0, z=2>";
            var coordinate = new Coordinate(coordinateString);
            Assert.AreEqual(-1, coordinate.X);
            Assert.AreEqual(0, coordinate.Y);
            Assert.AreEqual(2, coordinate.Z);
        }

        [Test]
        public void Can_create_planetary_system()
        {
            var moons = new List<string>
            {
                "<x=-1, y=0, z=2>",
                "<x=2, y=-10, z=-7>",
                "<x=4, y=-8, z=8>",
                "<x=3, y=5, z=-1>"
            };
            var planetarySystem = new PlanetarySystem(moons);
            Assert.AreEqual(-8, planetarySystem.Moons[2].Position.Y);
        }

        [Test, Ignore("Ignore")]
        public void Can_calculate_velocity_from_for_1_step()
        {
            var moons = new List<string>
            {
                "<x=-1, y=0, z=2>",
                "<x=2, y=-10, z=-7>",
                "<x=4, y=-8, z=8>",
                "<x=3, y=5, z=-1>"
            };
            var planetarySystem = new PlanetarySystem(moons);
            planetarySystem.TakeTimeSteps(1);
            //Check step deltas
            Assert.AreEqual(3, planetarySystem.Moons[0].StepDelta.X);
            Assert.AreEqual(-1, planetarySystem.Moons[0].StepDelta.Y);
            Assert.AreEqual(-3, planetarySystem.Moons[3].StepDelta.Y);
            Assert.AreEqual(1, planetarySystem.Moons[1].StepDelta.X);
            //Check velocity 
            Assert.AreEqual(3, planetarySystem.Moons[0].Velocity.X);
            Assert.AreEqual(-1, planetarySystem.Moons[0].Velocity.Y);
            Assert.AreEqual(-3, planetarySystem.Moons[3].Velocity.Y);
            //Check positions
            Assert.AreEqual(2, planetarySystem.Moons[0].Position.X);
            Assert.AreEqual(-1, planetarySystem.Moons[0].Position.Y);
            Assert.AreEqual(2, planetarySystem.Moons[3].Position.Y);
            Assert.AreEqual(3, planetarySystem.Moons[1].Position.X);
        }

        [Test]
        public void Can_calculate_velocity_from_for_2_stepsj()
        {
            var moons = new List<string>
            {
                "<x=-1, y=0, z=2>",
                "<x=2, y=-10, z=-7>",
                "<x=4, y=-8, z=8>",
                "<x=3, y=5, z=-1>"
            };
            var planetarySystem = new PlanetarySystem(moons);
            planetarySystem.TakeTimeSteps(2);
            //Check step deltas
            Assert.AreEqual(3, planetarySystem.Moons[0].Velocity.X);
            Assert.AreEqual(-2, planetarySystem.Moons[0].Velocity.Y);
            Assert.AreEqual(-6, planetarySystem.Moons[3].Velocity.Y);
            //Check positions
            Assert.AreEqual(5, planetarySystem.Moons[0].Position.X);
            Assert.AreEqual(-3, planetarySystem.Moons[0].Position.Y);
            Assert.AreEqual(-4, planetarySystem.Moons[3].Position.Y);
        }

        [Test]
        public void Can_calculate_total_energy()
        {
            var moons = new List<string>
            {
                "<x=-1, y=0, z=2>",
                "<x=2, y=-10, z=-7>",
                "<x=4, y=-8, z=8>",
                "<x=3, y=5, z=-1>"
            };
            var planetarySystem = new PlanetarySystem(moons);
            planetarySystem.TakeTimeSteps(10);
            Assert.AreEqual(179, planetarySystem.TotalEnergy);
        }

        [Test]
        public void Can_do_day_12_step_1()
        {
            var moons = new List<string>
            {
                "<x=0, y=6, z=1>",
                "<x=4, y=4, z=19>",
                "<x=-11, y=1, z=8>",
                "<x=2, y=19, z=15>"
            };
            var planetarySystem = new PlanetarySystem(moons);
            planetarySystem.TakeTimeSteps(1000);
            Assert.AreEqual(14809, planetarySystem.TotalEnergy);
        }

        [Test]
        public void Can_get_repeat_step_for_first_example()
        {
            var moons = new List<string>
            {
                "<x=-1, y=0, z=2>",
                "<x=2, y=-10, z=-7>",
                "<x=4, y=-8, z=8>",
                "<x=3, y=5, z=-1>"
            };
            var planetarySystem = new PlanetarySystem(moons);
            var repeatStep = planetarySystem.FindRepeatingPattern();
            Assert.AreEqual(2772, repeatStep);
        }

        [Test, Ignore("takes too long")]
        public void Can_get_repeat_step_for_second_example()
        {
            var moons = new List<string>
            {
                "<x=-8, y=-10, z=0>",
                "<x=5, y=5, z=10>",
                "<x=2, y=-7, z=3>",
                "<x=9, y=-8, z=-3>"
            };
            var planetarySystem = new PlanetarySystem(moons);
            var repeatStep = planetarySystem.FindRepeatingPattern();
            Assert.AreEqual(4686774924, repeatStep);
        }

        [Test]
        public void Play_with_heap()
        {
            var set = new SortedSet<Moon>();
            
        }


    }
}
