using System;
using OpenTK;

namespace ImpossibleLearning.Physics
{
	public class Rectangle : Body, ICollideable<Rectangle>
	{
		public Vector2 Min { get; protected set; }
		public Vector2 Max { get; protected set; }
		
		public float Density { get; protected set; }
		
		public override float Mass {
			get { return Density * (Max.X - Min.X) * (Max.Y - Min.Y); }
		}
		
		public override Vector2 Position
		{
			get { return Min; }
			set {
				Console.WriteLine("{0}, {1} => {2}, {3}", Min, Max, value, Max + value - Min);
				Max += value - Min;
				Min = value;
			}
		}
		
		public Rectangle(float x, float y, float width, float height)
			: this(new Vector2(x, y), new Vector2(x + width, y + height))
		{ }
		
		public Rectangle(Vector2 Min, float width, float height)
			: this(Min, new Vector2(Min.X + width, Min.Y + height))
		{ }
		
		public Rectangle(Vector2 min, Vector2 max)
		{
			Min = min;
			Max = max;
		}
		
		public Collision Collides(Rectangle b)
		{
			// if(!(b.Min.X <= Max.X && Min.X <= b.Max.X && b.Min.Y <= Max.Y && Min.Y <= b.Max.Y)) return null;
			
			// Calculate half extents along x axis for each object
			float aXExtent = (Max.X - Min.X) / 2;
			float bXExtent = (b.Max.X - b.Min.X) / 2;
				
			Vector2 normal = Min - b.Min;
			
			// Calculate overlap on x axis
			float xOverlap = aXExtent + bXExtent - Math.Abs(normal.X);
			
			// SAT test on x axis
			if(xOverlap > 0)
			{
				// Calculate half extents along x axis for each object
				float aYExtent = (Max.Y - Min.Y) / 2;
				float bYExtent = (b.Max.Y - b.Min.Y) / 2;
				
				// Calculate overlap on y axis
				float yOverlap = aYExtent + bYExtent - Math.Abs(normal.Y);
				
				// SAT test on y axis
				if(yOverlap > 0)
				{
					// Find out which axis is axis of least penetration
					if(xOverlap > yOverlap)
					{
						return normal.X < 0 ? new Collision(xOverlap, new Vector2(-1, 0)) : new Collision(xOverlap, new Vector2(1, 0));
					}
					else
					{
						return normal.Y < 0 ? new Collision(yOverlap, new Vector2(0, -1)) : new Collision(yOverlap, new Vector2(0, 1));
					}
				}
			}
			
			return null;
		}
	}
}
