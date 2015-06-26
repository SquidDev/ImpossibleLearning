using System;
using System.Collections.Generic;
using System.Linq;

using ImpossibleLearning.Game;
using ImpossibleLearning.Utils;

namespace ImpossibleLearning.Levels
{
	/// <summary>
	/// A level is a segment of code
	/// </summary>
	public class Level
	{
		public int Difficulty { get; protected set; }
		public Vector2i Exit { get; protected set; }
		protected Dictionary<Vector2i, TileType> Tiles = new Dictionary<Vector2i, TileType>();

		public Level(int difficulty, Dictionary<Vector2i, TileType> tiles, Vector2i exit)
		{
			Difficulty = difficulty;
			Exit = exit;
			Tiles = tiles;
		}
		
		/// <summary>
		/// Inject this level into the world
		/// </summary>
		/// <param name="world">The world to inject into</param>
		/// <param name="start">The start position</param>
		/// <returns>The end position</returns>
		public Vector2i Inject(World world, Vector2i start)
		{
			foreach(KeyValuePair<Vector2i, TileType> tile in Tiles)
			{
				world.Add(tile.Value.Create(start + tile.Key));
			}
			
			return Exit + start;
		}
		
		public override string ToString()
		{
			return String.Format("[Exit:{0}\n{1}\n]", Exit, Tiles.Format());
		}

	}
	
	public class LevelManager
	{
		public Vector2i NextEntrance { get; protected set; }
		public World World { get; protected set; }
		
		public LevelManager(World world)
		{
			NextEntrance = Vector2i.Zero;
			World = world;
		}
		
		public void Add(Level level)
		{
			NextEntrance = level.Inject(World, NextEntrance);
			NextEntrance = new Vector2i(NextEntrance.X + 1, NextEntrance.Y);
		}
	}
}
