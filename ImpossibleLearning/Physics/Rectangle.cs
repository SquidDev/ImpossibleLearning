using System;
using OpenTK;

namespace ImpossibleLearning.Physics
{
    public class Rectangle
    {
        public Vector2 Velocity { get; set; }
        public bool Fixed { get; protected set; }

        public Vector2 Min { get; protected set; }
        public Vector2 Max { get; protected set; }

        public Vector2 Position
        {
            get { return Min; }
            set
            {
                Max += value - Min;
                Min = value;
            }
        }

        public Rectangle(float x, float y, float width, float height)
            : this(new Vector2(x, y), new Vector2(x + width, y + height))
        {
        }

        public Rectangle(Vector2 Min, float width, float height)
            : this(Min, new Vector2(Min.X + width, Min.Y + height))
        {
        }

        public Rectangle(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }

        public bool Touches(Rectangle b)
        {
            return b.Min.X <= Max.X && Min.X <= b.Max.X && b.Min.Y <= Max.Y && Min.Y <= b.Max.Y;
        }

        public bool Collides(Rectangle b)
        {
            return b.Min.X < Max.X && Min.X < b.Max.X && b.Min.Y < Max.Y && Min.Y < b.Max.Y;
        }

        public void Resolve(Rectangle b)
        {
            if (!Collides(b)) return;

            float xOverlap = Math.Max(0, Math.Min(Max.X, b.Max.X) - Math.Max(Min.X, b.Min.X));
            float yOverlap = Math.Max(0, Math.Min(Max.Y, b.Max.Y) - Math.Max(Min.Y, b.Min.Y));

            if (xOverlap == 0 || yOverlap == 0) return;

            Vector2 vector = xOverlap > yOverlap ? new Vector2(0, yOverlap) : new Vector2(xOverlap, 0);
            if (!Fixed)
            {
                Position += vector;
            }
            else if (!b.Fixed)
            {
                b.Position -= vector;
            }
        }

        public override string ToString()
        {
            return string.Format("[Rectangle: {0}, {1}]", Min, Max);
        }
    }
}
