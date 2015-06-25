using System;
using System.Linq;
using System.Collections.Generic;
using OpenTK;

namespace ImpossibleLearning.Game
{
	public delegate void PlayerKilledHandler(object sender, EventArgs e);

	public class Character
	{
        public Vector2 Position { get; protected set; }
        public Vector2 Velocity { get; protected set; }

        public World World { get; private set; }

		public event PlayerKilledHandler Killed;

		public Character(World world)
		{ 
            Velocity = new Vector2(0.05f, 0f);
            Position = Vector2.Zero;
            World = world;
        }

        public void Update()
        {
            if (Position.Y < World.Tiles.Min(t => t.Key.Y) - 5)
            {
                Kill();
                return;
            }

            Position = Position + Velocity;
        }

		public void Jump()
		{
		}

		public void Kill()
		{
			if (Killed != null)
			{
				Killed.Invoke(this, new EventArgs());
			}
            Velocity = Vector2.Zero;
		}
	}
}

