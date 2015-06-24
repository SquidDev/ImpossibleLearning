using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace ImpossibleLearning.Game
{
    public enum TileType
    {
        Air,
        Spike,
        Wall,
    }

    public abstract class Tile
    {
        public Coordinate Position { get; private set; }

        public TileType Type { get; private set; }

        public Tile(Coordinate position, TileType type)
        {
            Position = position;
            Type = type;
        }

        /// <summary>
        /// Called before the character's collision handling.
        /// </summary>
        /// <param name="character">Character.</param>
        public virtual void Collide(Character character)
        { 
            // Kinda ugly.
            switch (Type)
            {
                case ImpossibleLearning.Game.TileType.Spike:
                    character.Kill();
                    break;
            }
        }
    }
}

