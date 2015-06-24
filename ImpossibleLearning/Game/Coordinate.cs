using System;

namespace ImpossibleLearning.Game
{
	public class Coordinate
	{
		public int X { get; private set; }
		public int Y { get; private set; }

		public Coordinate(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override int GetHashCode()
		{
            return X * 31 + Y;
		}

		public override bool Equals(object obj)
		{
			Coordinate coordinate = obj as Coordinate;
			if (coordinate == null) return false;

            return X == coordinate.X && Y == coordinate.Y;
		}

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", X, Y);
        }

        public static Coordinate operator +(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.X + b.X, a.Y + b.Y);
        }

        public static Coordinate operator -(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.X - b.X, a.Y - b.Y);
        }

        public static Coordinate operator *(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.X * b.X, a.Y * b.Y);
        }

        public static Coordinate operator *(int a, Coordinate b)
        {
            return new Coordinate(a * b.X, a * b.Y);
        }

        public static Coordinate operator *(Coordinate a, int b)
        {
            return new Coordinate(a.X * b, a.Y * b);
        }

        public static Coordinate operator /(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.X / b.X, a.Y / b.Y);
        }

        public static Coordinate operator /(int a, Coordinate b)
        {
            return new Coordinate(a / b.X, a / b.Y);
        }

        public static Coordinate operator /(Coordinate a, int b)
        {
            return new Coordinate(a.X / b, a.Y / b);
        }
	}
}

