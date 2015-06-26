using System;
using ImpossibleLearning.Game;
using ImpossibleLearning.Utils;

namespace ImpossibleLearning.Levels
{
	public enum TileType
	{
		Block,
		Spike,
		Unknown,
	}
	
	public static class TileExtensions
    {
    	public static Tile Create(this TileType type, Vector2i position)
    	{
    		switch(type)
    		{
    			case TileType.Block:
    				return new Block(position);
    			case TileType.Spike:
    				return new Spike(position);
    		}
    		
    		throw new ArgumentException("Unexpected type " + type);
    	}
    }
}
