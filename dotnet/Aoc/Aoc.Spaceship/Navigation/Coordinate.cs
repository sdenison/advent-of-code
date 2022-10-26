﻿using System;

namespace Aoc.Spaceship.Navigation
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public float? Slope => X == 0 ? (float?) null : Math.Abs(Y / X);

        public int Quadrant
        {
            get
            {
                if (X >= 0 && Y > 0)
                    return 1;
                if (X > 0 && Y <= 0)
                    return 2;
                if (X <= 0 && Y < 0)
                    return 3;
                return 4;
            }
        }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Coordinate obj)
        {
            return X == obj.X && Y == obj.Y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Coordinate);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}
