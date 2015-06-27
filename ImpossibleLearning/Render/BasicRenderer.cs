using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using ImpossibleLearning.Game;
using ImpossibleLearning.Levels;
using ImpossibleLearning.Physics;
using ImpossibleLearning.Utils;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace ImpossibleLearning.Render
{
    public class BasicRenderer : GameWindow
    {
        protected World world = new World();
        protected bool dead = false;
        
        LevelManager manager;
        Dictionary<int, Level> levels = LevelParser.FromLevels();

        public BasicRenderer()
        {
            manager = new LevelManager(world);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            VSync = VSyncMode.On;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
        }

        protected float previousPress = 0;

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[Key.Escape])
            {
                Exit();
                return;
            }

            if (world.Tiles.Count == 0 || world.Character.Position.X > world.Tiles.Keys.Max(x => x.X) - Width)
            {
                manager.Add(levels.Values.RandomElement());
            }
                
            if (dead) return;

            if (!Keyboard[Key.ControlLeft])
            {
                if (Keyboard[Key.Right])
                {
                    previousPress++;
                }
                else
                {
                    previousPress = 0;
                }

                if (previousPress < 10) return;
                previousPress = 0;
            }

            try
            {
                world.Update();
                if (Keyboard[Key.Space]) world.Character.Jump();
            }
            catch (CharacterKilledException error)
            {
                dead = true;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            int size = Width / world.Width;
            int width = Width / size, height = Height / size;
            GL.Ortho(0, width, height, 0, -1, 1);

            GL.PushMatrix();
            GL.Translate(-world.Character.Position.X - 0.5 + width / 2, -world.Character.Position.Y - 0.5 + height / 2, 0);

            GL.Begin(PrimitiveType.Quads);

            foreach (Tile tile in world.Tiles.Values)
            {
                if (tile is Block)
                {
                    GL.Color3(Color.FromArgb(50, 50, 50));
                }
                else if (tile is Spike)
                {
                    GL.Color3(Color.FromArgb(255, 140, 0));
                }
                else
                {
                    GL.Color3(Color.FromArgb(0, 0, 255));
                }

                GL.Vertex2(tile.Position.X, tile.Position.Y);
                GL.Vertex2(tile.Position.X, tile.Position.Y + 1);
                GL.Vertex2(tile.Position.X + 1, tile.Position.Y + 1);
                GL.Vertex2(tile.Position.X + 1, tile.Position.Y);
            }

            GL.Color3(Color.FromArgb(255, 175, 0));
            GL.Vertex2(world.Character.Position.X, world.Character.Position.Y);
            GL.Vertex2(world.Character.Position.X, world.Character.Position.Y + 1);
            GL.Vertex2(world.Character.Position.X + 1, world.Character.Position.Y + 1);
            GL.Vertex2(world.Character.Position.X + 1, world.Character.Position.Y);
            
            GL.End();
            
            
            foreach (Tile tile in world.Character.GetCollides())
            {
                GL.Begin(PrimitiveType.LineLoop);
            	
                if (world.Character.Collides(tile) != null)
                {
                    GL.Color3((byte)0, (byte)0, (byte)255);
                }
                else
                {
                    GL.Color3((byte)255, (byte)0, (byte)0);
                }
                GL.Vertex2(tile.Position.X, tile.Position.Y);
                GL.Vertex2(tile.Position.X, tile.Position.Y + 1);
                GL.Vertex2(tile.Position.X + 1, tile.Position.Y + 1);
                GL.Vertex2(tile.Position.X + 1, tile.Position.Y);
                
                GL.End();
            }

            GL.PopMatrix();

            SwapBuffers();
        }
    }
}

