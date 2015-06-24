using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace ImpossibleLearning
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			using (var game = new GameWindow())
			{
				game.Load += (sender, e) =>
				{
					// setup settings, load textures, sounds
					game.VSync = VSyncMode.On;
				};

				game.Resize += (sender, e) =>
				{
					GL.Viewport(0, 0, game.Width, game.Height);
				};

				int offset = 0;
				game.UpdateFrame += (sender, e) =>
				{
					// add game logic, input handling
					if (game.Keyboard[Key.Escape])
					{
						game.Exit();
					}
					offset += 1;
				};

				game.RenderFrame += (sender, e) =>
				{
					// render graphics
					GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

					GL.MatrixMode(MatrixMode.Projection);
					GL.LoadIdentity();
					// GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

					GL.Begin(PrimitiveType.Triangles);

					GL.Rotate(offset / 360.0f, 0, 1, 0);
					GL.Color3(Color.MidnightBlue);
					GL.Vertex3(-1.0f, 1.0f, 1.0f);
					GL.Color3(Color.SpringGreen);
					GL.Vertex3(0.0f, -1.0f, 0.0f);
					GL.Color3(Color.Ivory);
					GL.Vertex3(1.0f, 1.0f, 1.0f);

					GL.End();

					game.SwapBuffers();
				};

				// Run the game at 60 updates per second
				game.Run(60.0);
			}
		}
	}
}
