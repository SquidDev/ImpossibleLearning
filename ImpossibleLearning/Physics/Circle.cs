using System;
using OpenTK;

namespace ImpossibleLearning.Physics
{
	public class Circle : Body, ICollideable<Circle>
	{
		public Vector2 Center { get; protected set; }
		public float Radius { get; protected set; }

		public override Vector2 Position
		{
			get { return Center; }
			set { Center = value; }
		}
		
		public Circle(Vector2 center, float radius)
		{
			Center = center;
			Radius = radius;
		}
		
		public Collision Collides(Circle other)
		{
			float radiusSquared = (float)Math.Pow(Radius + other.Radius, 2);
			Vector2 normal = other.Center - Center;
			
			if(radiusSquared < normal.LengthSquared) return null;
			
			float d = normal.Length;
			return d != 0 ? 
				new Collision(radiusSquared - d, normal / d) :
				
				// They are at the same position. Make something up.
				new Collision(Radius, new Vector2(1, 0));
		}
	}
}
