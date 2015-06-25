using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using ImpossibleLearning.Render;

namespace ImpossibleLearning
{
	class MainClass
	{
		public static void Main(string[] args)
		{
            using (var game = new BasicRenderer())
			{
				// Run the game at 60 updates per second
				game.Run(60.0);
			}
		}
	}
}
