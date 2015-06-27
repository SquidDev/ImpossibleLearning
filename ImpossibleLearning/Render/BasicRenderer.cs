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
        protected World world;
        
        LevelManager manager;
        Dictionary<int, Level> levels = LevelParser.FromLevels();

        protected int deadCooldown = 0;

        public void Setup()
        {
            world = new World();
            manager = new LevelManager(world);
            manager.Add(levels[0]);
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

            if (deadCooldown > 0)
            {
                deadCooldown--;
                return;
            }
            else if (deadCooldown == 0)
            {
                Setup();
                deadCooldown--;
            }

            if (Keyboard[Key.ControlLeft])
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

            if (world.Tiles.Count == 0 || world.Character.Position.X > world.Tiles.Keys.Max(x => x.X) - Width)
            {
                manager.Add(levels.Values.RandomElement());
            }

            try
            {
                world.Update();
                if (Keyboard[Key.Space]) world.Character.Jump();
            }
            catch (CharacterKilledException err)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You died: {0}", err.Message);
                Console.ForegroundColor = ConsoleColor.White;

                deadCooldown = 30;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            if (world == null) return;

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

                Draw(tile);
            }

            GL.Color3(Color.FromArgb(255, 175, 0));
            Draw(world.Character);
            
            GL.End();
            
            
            foreach (Tile tile in world.Character.GetAdjacent())
            {
                GL.Begin(PrimitiveType.LineLoop);
            	
                if (tile.Collides(world.Character))
                {
                    GL.Color3((byte)255, (byte)0, (byte)0);
                }
                else if (tile.Touches(world.Character))
                {
                    GL.Color3((byte)0, (byte)0, (byte)255);
                }
                else
                {
                    GL.Color3((byte)0, (byte)255, (byte)0);
                }

                Draw(tile);
                
                GL.End();
            }

            GL.PopMatrix();

            SwapBuffers();
        }

        protected void Draw(ImpossibleLearning.Physics.Rectangle rect)
        {
            GL.Vertex2(rect.Min.X, rect.Min.Y);
            GL.Vertex2(rect.Min.X, rect.Max.Y);
            GL.Vertex2(rect.Max.X, rect.Max.Y);
            GL.Vertex2(rect.Max.X, rect.Min.Y);
        }
    }
}

