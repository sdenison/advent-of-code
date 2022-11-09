using System.Collections.Generic;
using System.Linq;

namespace Aoc.Spaceship.Navigation.Moons
{
    public class PlanetarySystem
    {
        public IList<Moon> Moons { get; set; }
        public int TotalEnergy
        {
            get { return Moons.Select(x => x.TotalEnergy).Sum(); }
        }

        public PlanetarySystem(IList<string> moonCoordinates)
        {
            Moons = new List<Moon>();
            foreach(var moon in moonCoordinates)
                Moons.Add(new Moon(moon));
        }

        public void TakeTimeSteps(int stepsToTake)
        {
            for (int i = 0; i < stepsToTake; i++)
            {
                foreach (var moon in Moons)
                {
                    var moonDelta = new Coordinate(0, 0, 0);
                    foreach (var moonToCompare in Moons)
                    {
                        if (moonToCompare != moon)
                        {
                            if (moon.Position.X < moonToCompare.Position.X)
                                moonDelta.X++;
                            if (moon.Position.X > moonToCompare.Position.X)
                                moonDelta.X--;
                            if (moon.Position.Y < moonToCompare.Position.Y)
                                moonDelta.Y++;
                            if (moon.Position.Y > moonToCompare.Position.Y)
                                moonDelta.Y--;
                            if (moon.Position.Z < moonToCompare.Position.Z)
                                moonDelta.Z++;
                            if (moon.Position.Z > moonToCompare.Position.Z)
                                moonDelta.Z--;
                        }
                    }
                    moon.StepDelta = moonDelta;
                    moon.Velocity.X += moon.StepDelta.X;
                    moon.Velocity.Y += moon.StepDelta.Y;
                    moon.Velocity.Z += moon.StepDelta.Z;
                }
                foreach (var moon in Moons)
                {
                    moon.Position.X += moon.Velocity.X;
                    moon.Position.Y += moon.Velocity.Y;
                    moon.Position.Z += moon.Velocity.Z;
                }
            }
        }
    }
}
