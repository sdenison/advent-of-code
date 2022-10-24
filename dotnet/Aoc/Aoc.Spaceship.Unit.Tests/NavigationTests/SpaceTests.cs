using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aoc.Spaceship.Navigation;
using NUnit.Framework;

namespace Aoc.Spaceship.Unit.Tests.NavigationTests
{
    [TestFixture]
    public class SpaceTests
    {
        [Test]
        public void Can_create_space()
        {
            char[,] simpleMap = new char[,]
            {
                {'.', '.'},
                {'.', '.'},
            };
            var space = new Space(simpleMap);
        }
    }
}
