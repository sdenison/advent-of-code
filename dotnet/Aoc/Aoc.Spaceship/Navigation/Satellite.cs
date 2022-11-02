using System.Collections.Generic;

namespace Aoc.Spaceship.Navigation
{
    public class Satellite
    {
        private int _orbitLevel;
        public IList<Satellite> Satellites { get; }
        public string Name { get; }
        public int Orbits { get
        {
            var orbits = 0;
            foreach (var satellite in Satellites)
                orbits += satellite.Orbits;
            orbits += _orbitLevel;
            return orbits;
        }}

        public Satellite(string name, int orbitLevel)
        {
            Name = name;
            Satellites = new List<Satellite>();
            _orbitLevel = orbitLevel;           
        }

        public bool AddOrbit(string orbitString)
        { 
            var currentSatelliteName = orbitString.Split(')')[0];
            var orbitingSatelliteName = orbitString.Split(')')[1];
            if (currentSatelliteName == Name)
            {
                Satellites.Add(new Satellite(orbitingSatelliteName, _orbitLevel + 1));
                return true;
            }
            foreach (var orbitingSatellite in Satellites)
            {
                if (orbitingSatellite.AddOrbit(orbitString))
                    return true;
            }
            return false;
        }
    }
}
