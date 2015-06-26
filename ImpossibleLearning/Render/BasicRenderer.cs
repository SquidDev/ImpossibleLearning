using System;
using System.Collections.Generic;
using System.Drawing;
using ImpossibleLearning.Game;
using ImpossibleLearning.Levels;
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
        
        public BasicRenderer()
        {
        	LevelManager manager = new LevelManager(world);
        	var levels = LevelParser.FromLevels();
        	
        	for(int i = 0; i < 10; i++)
        	{
        		manager.Add(levels.Values.RandomElement());
        	}
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

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[Key.Escape])
            {
                Exit();
                return;
            }

            world.Update();

            if (Keyboard[Key.Space])
            {
                world.Character.Jump();
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
            GL.Vertex2(world.Character.Position.X + 1, world.Character.Position.Y+1);
            GL.Vertex2(world.Character.Position.X+1, world.Character.Position.Y);

            GL.End();

            GL.PopMatrix();

            SwapBuffers();
        }
    }
}

