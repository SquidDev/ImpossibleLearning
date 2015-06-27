using System;
using OpenTK;

namespace ImpossibleLearning.Physics
{
	/// <summary>
	/// Description of Resolver.
	/// </summary>
	public static class Resolver
	{
		public static void ResolveCollision<T>(T a, T b, Collision collision)
			where T : Body, ICollideable<T>
		{
			// Calculate relative velocity
			Vector2 rv = a.Velocity - b.Velocity;
			
			// Calculate relative velocity in terms of the normal direction
			float velocityNormal = Vector2.Dot(rv, collision.Normal);
			
			if(velocityNormal > 0) return; // Do not resolve if velocities are separating
			
			// Calculate restitution
			float restitution = Math.Min(a.Restitution, b.Restitution);
			
			// Calculate impulse scalar
			float invA = a.InverseMass, invB = b.InverseMass;
			float impulse = -(1 + restitution) * velocityNormal / invA + invB;
			
			// Apply impulse
			Vector2 impulseVector = impulse * collision.Normal;
			
			a.Velocity -= invA * impulseVector;
			b.Velocity += invB * impulseVector;
		}
		
		public static void Correct<T>(T a, T b, Collision collision)
			where T : Body, ICollideable<T>
		{
			const float percent = 0.2f;
			const float slop = 0.01f;
				
			float invA = a.InverseMass, invB = b.InverseMass;
			Vector2 correction = Math.Max(collision.Penetration - slop, 0.0f) / (invA + invB) * percent * collision.Normal;
			if(!a.Fixed) a.Position -= invA * correction;
			if(!b.Fixed) b.Position += invB * correction;
		}
		
		public static bool Handle<T>(T a, T b)
			where T : Body, ICollideable<T>
		{
			Collision collision = a.Collides(b);
			if(collision != null) {
				Console.WriteLine("Collides");
				ResolveCollision(a, b, collision);
				Correct(a, b, collision);
				return true;
			}
			
			return false;
		}
	}
}
