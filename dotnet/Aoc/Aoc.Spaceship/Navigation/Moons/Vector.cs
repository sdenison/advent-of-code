using System;
using System.Collections.Generic;

namespace Aoc.Spaceship.Navigation.Moons
{
    public class Vector : IComparer<Vector>, IEqualityComparer<Vector>, IComparable<Vector>
    {
        public int Xposition { get; set; }
        public int Yposition { get; set; }
        public int Zposition { get; set; }
        public int Xvelocity { get; set; }
        public int Yvelocity { get; set; }
        public int Zvelocity { get; set; }

        public Vector(int xposition, int yposition, int zposition, int xvelocity, int yvelocity, int zvelocity)
        {
            Xposition = xposition;
            Yposition = yposition;
            Zposition = zposition;
            Xvelocity = xvelocity;
            Yvelocity = yvelocity;
            Zvelocity = zvelocity;
        }

        public int Compare(Vector x, Vector y)
        {
            if (x.Xposition > y.Xposition)
                return 1;
            if (y.Xposition > x.Xposition)
                return -1;
            if (x.Yposition > y.Yposition)
                return 1;
            if (y.Yposition > x.Yposition)
                return -1;
            if (x.Zposition > y.Zposition)
                return 1;
            if (y.Zposition > x.Zposition)
                return -1;
            if (x.Xvelocity > y.Xvelocity)
                return 1;
            if (y.Xvelocity > x.Xvelocity)
                return -1;
            if (x.Yvelocity > y.Yvelocity)
                return 1;
            if (y.Yvelocity > x.Yvelocity)
                return -1;
            if (x.Zvelocity > y.Zvelocity)
                return 1;
            if (y.Zvelocity > x.Zvelocity)
                return -1;
            return 0;
        }

        public bool Equals(Vector x, Vector y)
        {
            return Compare(x, y) == 0;
        }

        public int GetHashCode(Vector obj)
        {
            return (int) (Math.Abs(Xposition) * 10000000000 + Math.Abs(Yposition) * 100000000 + Math.Abs(Zposition) * 1000000 +
                          Math.Abs(Xvelocity) * 10000 + Math.Abs(Yvelocity) * 100 + Math.Abs(Zvelocity));
        }

        public int CompareTo(Vector other)
        {
            return Compare(this, other);
        }
    }
}
