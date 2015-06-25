using System;

namespace ImpossibleLearning.Game
{
	public class Vector2i
	{
		public int X { get; protected set; }
        public int Y { get; protected set; }

		public Vector2i(int x, int y)
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
			Vector2i coordinate = obj as Vector2i;
			if (coordinate == null) return false;

            return X == coordinate.X && Y == coordinate.Y;
		}

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", X, Y);
        }

        public static Vector2i operator +(Vector2i a, Vector2i b)
        {
            return new Vector2i(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2i operator -(Vector2i a, Vector2i b)
        {
            return new Vector2i(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2i operator *(Vector2i a, Vector2i b)
        {
            return new Vector2i(a.X * b.X, a.Y * b.Y);
        }

        public static Vector2i operator *(int a, Vector2i b)
        {
            return new Vector2i(a * b.X, a * b.Y);
        }

        public static Vector2i operator *(Vector2i a, int b)
        {
            return new Vector2i(a.X * b, a.Y * b);
        }

        public static Vector2i operator /(Vector2i a, Vector2i b)
        {
            return new Vector2i(a.X / b.X, a.Y / b.Y);
        }

        public static Vector2i operator /(int a, Vector2i b)
        {
            return new Vector2i(a / b.X, a / b.Y);
        }

        public static Vector2i operator /(Vector2i a, int b)
        {
            return new Vector2i(a.X / b, a.Y / b);
        }
	}
}

