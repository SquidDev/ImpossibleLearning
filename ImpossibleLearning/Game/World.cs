using System;
using System.Collections.Generic;
using System.Linq;
using ImpossibleLearning.Utils;

namespace ImpossibleLearning.Game
{
	public class World
	{
        public Dictionary<Coordinate, Tile> Tiles = new Dictionary<Coordinate, Tile>();
        public Character Character = new Character();

        public int Width { get; private set; }

        public void Update()
        {
            int cull = (int)(Character.Position.X - (Width / 2)) - 1;
            Tiles.RemoveAll(pair => pair.Key.X < cull);
        }
	}
}

