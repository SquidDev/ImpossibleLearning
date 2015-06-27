using System;
using System.Collections.Generic;
using System.Linq;
using ImpossibleLearning.Utils;

namespace ImpossibleLearning.Game
{
	public class World
	{
        public Dictionary<Vector2i, Tile> Tiles = new Dictionary<Vector2i, Tile>();
        public Character Character;

        public int Width { get; private set; }

        public World()
        {
            Width = 20;
            Character = new Character(this);
        }

        public void Update()
        {
            // int cull = (int)(Character.Position.X - (Width / 2)) - 1;
            // Tiles.RemoveAll(pair => pair.Key.X < cull);

            foreach (Tile Tile in Tiles.Values)
            {
                Tile.Update();
            }

            Character.Update();
        }

        public void Add(Tile tile)
        {
            Tiles.Add(tile.Position, tile);
        }
	}
}

