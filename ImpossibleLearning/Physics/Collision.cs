using System;
using OpenTK;

namespace ImpossibleLearning.Physics
{
    public interface ICollideable<T>
		where T : Body
    {
        /// <summary>
        /// Check for a collision
        /// </summary>
        /// <param name="other">The object to collide with</param>
        /// <returns>If a collision occured</returns>
        bool Collides(T other);

        /// <summary>
        /// Resolve a collision
        /// </summary>
        /// <param name="other">The object to collide with</param>
        void Resolve(T other);
    }

    public abstract class Body
    {
        public Vector2 Velocity { get; set; }

        public abstract Vector2 Position { get; set; }

        public bool Fixed { get; protected set; }

        public Body()
        {
            Velocity = Vector2.Zero;
            Fixed = false;
        }
    }
}
