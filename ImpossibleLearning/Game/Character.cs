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

        public Character(World world)
            : base(0, 0, 1, 1)
        { 
            Velocity = new Vector2(0.2f, 0f);
            World = world;
        }

        public void Update()
        {
            if (World.Tiles.Count > 0 && Position.Y > World.Tiles.Max(t => t.Key.Y) + 5)
            {
                Kill("Fell out of the world");
                return;
            }
                
            Position += Velocity;

            foreach (Tile tile in GetCollides().Where(t => t.Collides(this)))
            {
                tile.Resolve(this);
                tile.Collide(this);
            }
            
            if (GetAdjacent().Where(t => t.Touches(this)).Where(t => t.Min.Y <= Min.X).Count() == 0)
            {
                Velocity -= new Vector2(0, -0.015f);
            }
            else
            {
                Velocity = new Vector2(Velocity.X, 0);
            }
        }

        public IEnumerable<Tile> GetAdjacent()
        {
            int x = (int)Math.Floor(Position.X), y = (int)Math.Floor(Position.Y);
        	
            List<Tile> tiles = new List<Tile>();
            Tile t;
        	
            t = World.Tiles.GetOrDefault(new Vector2i(x, y));
            if (t != null) tiles.Add(t);
        	
            t = World.Tiles.GetOrDefault(new Vector2i(x + 1, y));
            if (t != null) tiles.Add(t);
        	
            t = World.Tiles.GetOrDefault(new Vector2i(x + 1, y + 1));
            if (t != null) tiles.Add(t);
        	
            t = World.Tiles.GetOrDefault(new Vector2i(x, y + 1));
            if (t != null) tiles.Add(t);
        	
            return tiles;
        }

        public IEnumerable<Tile> GetCollides()
        {
            return GetAdjacent().Where(tile => tile.Collides(this)).ToList();
        }

        public void Jump()
        {
            if (GetAdjacent().Where(t => t.Touches(this)).Where(t => t.Min.Y <= Min.X).Count() > 0) Velocity = new Vector2(Velocity.X, Velocity.Y - 0.30f);
        }

        public void Kill(String message = "Because")
        {
            throw new CharacterKilledException(message, this);
        }
    }

    public class CharacterKilledException : Exception
    {
        public Character Character { get; private set; }

        public CharacterKilledException(String message, Character character)
            : base(message)
        {
            Character = Character;
        }
    }
}

