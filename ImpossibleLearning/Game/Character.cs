using System;
using OpenTK;

namespace ImpossibleLearning.Game
{
	public delegate void PlayerKilledHandler(object sender, EventArgs e);

	public class Character
	{
        public Vector2 Position { get; protected set; }
        public Vector2 Velocity { get; protected set; }

		public event PlayerKilledHandler Killed;

		public Character()
		{ 
            Velocity = Vector2.Zero;
            Position = Vector2.Zero;
        }

        public void Update()
        {
            
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
		}
	}
}

