using System;
using System.Collections.Generic;
using System.Linq;
using ImpossibleLearning.Utils;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Common;

namespace ImpossibleLearning.Game
{
	public class Stage
	{
        public Dictionary<Vector2i, Tile> Tiles = new Dictionary<Vector2i, Tile>();
        public Character Character;

        public int Width { get; private set; }

        public World World { get; private set; }

        public Stage()
        {
            Width = 20;
            World = new World(Vector2.Zero);
            World.Gravity = new Vector2(0, 0.1f);
            Character = new Character(this);
        }

        public void Update()
        {
            int cull = (int)(Character.Body.Position.X - (Width / 2)) - 1;
            Tiles.RemoveAll(pair => pair.Key.X < cull);

            foreach (Tile Tile in Tiles.Values)
            {
                Tile.Update();
            }

            if(Tiles.Count > 0 && Character.Body.Position.Y < Tiles.Min(t => t.Key.Y) - 5)
            {
                Character.Kill();
            }

            World.Step(1);
        }

        public void Add(Tile tile)
        {
            Tiles.Add(tile.Position, tile);
            tile.Setup(this);
        }
	}
}

