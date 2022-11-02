using System.Collections.Generic;

namespace Aoc.Spaceship.Navigation
{
    public class OrbitMap
    {
        public Satellite CenterOfMass { get; }

        public OrbitMap(IList<string> mapInput)
        {
            CenterOfMass = new Satellite("COM", 0);
            while (mapInput.Count > 0)
            {
                var processedOrbits = new List<string>();
                foreach(var orbit in mapInput)
                {
                    if (CenterOfMass.AddOrbit(orbit))
                    {
                        processedOrbits.Add(orbit);
                    }
                }
                foreach (var processedOrbit in processedOrbits)
                {
                    mapInput.Remove(processedOrbit);
                }
            }
        }
    }
}
