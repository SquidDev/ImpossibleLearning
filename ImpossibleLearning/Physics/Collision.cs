using System;
using OpenTK;

namespace ImpossibleLearning.Physics
{
	public interface ICollideable<T>
		where T : Body
	{
		/// <summary>
		/// Resolve a collision
		/// </summary>
		/// <param name="other">The object to collide with</param>
		/// <returns>The collision information or <code>null</code> if none occurs.</returns>
		Collision Collides(T other);
	}
	
	/// <summary>
	/// The result of a collision
	/// </summary>
	public class Collision
	{
		public float Penetration { get; private set; }
		public Vector2 Normal { get; private set; }
		
		public Collision(float penetration, Vector2 normal)
		{
			Penetration = penetration;
			Normal = normal;
		}
	}
	
	public abstract class Body
	{
		public Vector2 Velocity { get; set; }
		
		public float Restitution { get; protected set; }
		public bool Fixed { get; protected set; }
		public abstract float Mass { get; }
		
		public abstract Vector2 Position { get; set; }
		
		public float InverseMass
		{
			get 
			{
				float mass = Mass;
				return mass == 0 ? 0 : 1  / mass;
			}
		}
		
		public Body()
		{
			Velocity = Vector2.Zero;
			Restitution = 0.0f; 
			Fixed = false;
		}
	}
}
