using System;
using System.Drawing;
using ImpossibleLearning.Utils;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace ImpossibleLearning.Game
{
	public abstract class Tile : ImpossibleLearning.Physics.Rectangle
    {
        public new Vector2i Position { get; private set; }

        public Tile(Vector2i position) : base(position.X, position.Y, 1, 1)
        {
            Position = position;
            Fixed = true;
        }

        /// <summary>
        /// Called before the character's collision handling.
        /// </summary>
        /// <param name="character">Character.</param>
        public virtual void Collide(Character character)
        { }

        public virtual void Update() { }
    }

    public class Block : Tile
    {
        public Block(Vector2i position) : base(position) 
        { }
    }

    public class Spike : Tile
    {
        public Spike(Vector2i position) : base(position) 
        { }

        public override void Collide(Character character)
        {
            base.Collide(character);
            character.Kill("Spikey");
        }
    }
}

