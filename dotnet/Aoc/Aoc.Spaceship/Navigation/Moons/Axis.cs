using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aoc.Spaceship.Navigation.Moons
{
    public class Axis
    {
        public AxisValue AxisValue { get; set; }
        public SortedDictionary<long, SortedSet<long>> StepHistory { get; set; }

        public Axis(AxisValue axisValue)
        {
            AxisValue = axisValue;
            StepHistory = new SortedDictionary<long, SortedSet<long>>();
        }

        public async Task TakeSteps(int stepsToTake)
        {

        }
    }
}
