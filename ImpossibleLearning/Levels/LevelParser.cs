using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

using ImpossibleLearning.Utils;

namespace ImpossibleLearning.Levels
{
	/// <summary>
	/// Parses a level from a string
	/// </summary>
	public class LevelParser
	{
		public static Dictionary<int, Level> Parse(XElement element)
		{
			Dictionary<int, Level> levels = new Dictionary<int, Level>();
			
			foreach(XElement node in element.Elements("Level"))
			{
				int id = Int32.Parse(node.Attribute("Id").Value);
				int difficulty = Int32.Parse(node.Attribute("Difficulty").Value);
				
				int width = node.Element("Row").Value.Length;
				#region Find start and end
				int index = 0, start = -1, end = -1;
				foreach(String contents in node.Elements("Row").Select(r => r.Value))
				{
					if(contents.StartsWith("="))
					{
						if(start > -1) throw new ArgumentException("Multiple starts in #" + id);
						start = index;
					}
					
					if(contents.EndsWith("="))
					{
						if(end > -1) throw new ArgumentException("Multiple ends in #" + id);
						end = index;
					}
					
					index++;
				}
				
				if(start == -1) throw new ArgumentException("No start in #" + id);
				if(end == -1) throw new ArgumentException("No end in #" + id);
				#endregion
				
				Dictionary<Vector2i, TileType> tiles = node
					.Elements("Row")
					.WithIndex((y, row) => row.Value
						.WithIndex((x, c) => new KeyValuePair<Vector2i, TileType>(new Vector2i(x, y - start), FromChar(c)))
						.Where(t => t.Value != TileType.Unknown)
					)
					.SelectMany(x => x)
					.ToDictionary(x => x.Key, x => x.Value);
				
				levels.Add(id, new Level(difficulty, tiles, new Vector2i(width - 1, end - start)));
			}
			
			return levels;
		}
		
		public static Dictionary<int, Level> FromLevels()
		{
			using(StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("ImpossibleLearning.Levels.Levels.xml"))) {
				return Parse(XElement.Parse(reader.ReadToEnd()));
			}
		}
		
		public static TileType FromChar(char character)
		{
			switch(character)
			{
				case '=':
				case ' ':
					return TileType.Unknown;
					
				case 'X':
					return TileType.Block;
				case '^':
					return TileType.Spike;
			}
			
			throw new ArgumentException("Unknown tile " + character);
		}
	}
}
 