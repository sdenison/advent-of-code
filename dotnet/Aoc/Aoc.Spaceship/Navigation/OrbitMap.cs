using System.Collections.Generic;
using System.Linq;

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

        public IList<Satellite> GetPath(string name)
        {
            return CenterOfMass.GetPath(name).ToArray().ToList();
        }

        public Satellite GetCommonAncestor(IList<Satellite> path1, IList<Satellite> path2)
        {
            Satellite commonAncestor = null;
            for (var i=0; i<path2.Count; i++)
            {
                if (path1[i] == path2[i])
                    commonAncestor = path1[i];
                else
                    return commonAncestor;
            }

            return commonAncestor;
        }
    }
}
