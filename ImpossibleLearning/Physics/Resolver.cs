using System;
using OpenTK;

namespace ImpossibleLearning.Physics
{
	/// <summary>
	/// Description of Resolver.
	/// </summary>
	public static class Resolver
	{
		public static bool Handle<T>(T a, T b)
			where T : Body, ICollideable<T>
		{
			return a.Collides(b);
		}
	}
}
