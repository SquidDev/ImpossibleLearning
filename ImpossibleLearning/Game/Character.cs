using System;
using System.Collections.Generic;
using System.Linq;

using ImpossibleLearning.Physics;
using ImpossibleLearning.Utils;
using OpenTK;

namespace ImpossibleLearning.Game
{
	public class Character : Rectangle
	{
        public World World { get; private set; }

        public Character(World world) : base(0, 0, 1, 1)
		{ 
            Velocity = new Vector2(0.1f, 0f);
            Density = 1;
            World = world;
        }

        public void Update()
        {
        	if(World.Tiles.Count > 0 && Position.Y > World.Tiles.Max(t => t.Key.Y) + 5)
        	{
                Kill();
                return;
        	}

        	Console.WriteLine("{0} @ {1}", Position, Velocity);
            Position += Velocity;
            
            bool colliding = false;
            foreach(Tile tile in GetCollides(Position).Where(t => Resolver.Handle<Rectangle>(this, t)))
            {
            	tile.Collide(this);
            	colliding = true;
            }
            
            if(!colliding) 
            {
            	Velocity -= new Vector2(0, -0.01f);
            }
            else
            {
            	Velocity = new Vector2(Velocity.X, 0);
            }
        }
        
        public IEnumerable<Tile> GetCollides(Vector2 position)
        {
        	int x = (int)Math.Floor(position.X), y = (int)Math.Floor(position.Y);
        	
        	List<Tile> tiles = new List<Tile>();
        	Tile t;
        	
        	t = World.Tiles.GetOrDefault(new Vector2i(x, y));
        	if(t != null) tiles.Add(t);
        	
        	t = World.Tiles.GetOrDefault(new Vector2i(x + 1, y));
        	if(t != null) tiles.Add(t);
        	
        	t = World.Tiles.GetOrDefault(new Vector2i(x + 1, y + 1));
        	if(t != null) tiles.Add(t);
        	
        	t = World.Tiles.GetOrDefault(new Vector2i(x, y + 1));
        	if(t != null) tiles.Add(t);
        	
        	return tiles.Where(tile => Collides(position.X, position.Y, tile.Position.X, tile.Position.Y));
        }
        
        protected bool Collides(float x, float y, float oX, float oY)
        {
        	return oX <= x + 1 && x <= oX + 1 && oY <= y + 1 && y <= oY + 1;
        }

		public void Jump()
		{
			if(GetCollides(Position).Count() > 0) Velocity = new Vector2(Velocity.X, Velocity.Y - 0.25f);
		}

		public void Kill()
		{
			// throw new CharacterKilledException(this);
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

