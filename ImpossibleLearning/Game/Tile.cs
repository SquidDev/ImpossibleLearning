using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace ImpossibleLearning.Game
{
    public abstract class Tile
    {
        public Vector2i Position { get; private set; }

        public Tile(Vector2i position)
        {
            Position = position;
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
            character.Kill();
        }
    }
}

