using System;
using System.Collections.Generic;

namespace Aoc.Spaceship.Navigation.Moons
{
    public class Snapshot : IComparer<Snapshot>, IEqualityComparer<Snapshot>, IComparable<Snapshot>
    {
        public Vector Vector0 { get; set; }
        public Vector Vector1 { get; set; }
        public Vector Vector2 { get; set; }
        public Vector Vector3 { get; set; }

        public Snapshot(Vector vector0, Vector vector1, Vector vector2)
        {
            Vector0 = vector0;
            Vector1 = vector1;
            Vector2 = vector2;
            //Vector3 = vector3;
        }

        public int Compare(Snapshot x, Snapshot y)
        {
            if (x.Vector0.Compare(x.Vector0, y.Vector0) != 0)
                return x.Vector0.Compare(x.Vector0, y.Vector0);
            if (x.Vector1.Compare(x.Vector1, y.Vector1) != 0)
                return x.Vector1.Compare(x.Vector1, y.Vector1);
            if (x.Vector2.Compare(x.Vector2, y.Vector2) != 0)
                return x.Vector2.Compare(x.Vector2, y.Vector2);
            //if (x.Vector3.Compare(x.Vector3, y.Vector3) != 0)
            //    return x.Vector3.Compare(x.Vector3, y.Vector3);
            return 0;
        }

        public bool Equals(Snapshot x, Snapshot y)
        {
            return Compare(x, y) == 0;
        }

        public int GetHashCode(Snapshot obj)
        {
            return (int) (Vector0.GetHashCode() + Vector1.GetHashCode() + Vector2.GetHashCode() +
                          Vector3.GetHashCode());
        }

        public int CompareTo(Snapshot other)
        {
            return Compare(this, other);
        }
    }
}
