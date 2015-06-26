using System;
using System.Linq;
using System.Collections.Generic;
using OpenTK;

namespace ImpossibleLearning.Game
{
	public class Character
	{
        public Vector2 Position { get; protected set; }
        public Vector2 Velocity { get; protected set; }

        public World World { get; private set; }

		public Character(World world)
		{ 
            Velocity = new Vector2(0.05f, 0f);
            Position = Vector2.Zero;
            World = world;
        }

        public void Update()
        {
        	if(World.Tiles.Count > 0 && Position.Y < World.Tiles.Min(t => t.Key.Y) - 5)
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
			throw new CharacterKilledException(this);
		}
	}
	
	public class CharacterKilledException : Exception
	{
		public Character Character { get; private set; }
		
		public CharacterKilledException(Character character)
		{
			Character = Character;
		}
	}
}

