using System.Collections.Generic;

namespace Aoc.Spaceship.Navigation
{
    public class Satellite
    {
        public int OrbitDepth { get; }
        public IList<Satellite> Satellites { get; }
        public string Name { get; }
        public int Orbits { get
        {
            var orbits = 0;
            foreach (var satellite in Satellites)
                orbits += satellite.Orbits;
            orbits += OrbitDepth;
            return orbits;
        }}

        public Satellite(string name, int orbitDepth)
        {
            Name = name;
            Satellites = new List<Satellite>();
            OrbitDepth = orbitDepth;           
        }

        public bool AddOrbit(string orbitString)
        { 
            var currentSatelliteName = orbitString.Split(')')[0];
            var orbitingSatelliteName = orbitString.Split(')')[1];
            if (currentSatelliteName == Name)
            {
                Satellites.Add(new Satellite(orbitingSatelliteName, OrbitDepth + 1));
                return true;
            }
            foreach (var orbitingSatellite in Satellites)
                if (orbitingSatellite.AddOrbit(orbitString))
                    return true;
            return false;
        }

        public Stack<Satellite> GetPath(string name)
        {
            foreach (var satellite in Satellites)
            {
                if (satellite.Name == name)
                {
                    var path = new Stack<Satellite>();
                    path.Push(this);
                    return path;
                }
                var proposedPath = satellite.GetPath(name);
                if (proposedPath != null)
                {
                    proposedPath.Push(this);
                    return proposedPath;
                }
            }
            return null;
        }
    }
}
