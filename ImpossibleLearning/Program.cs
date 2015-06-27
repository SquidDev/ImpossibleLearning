using System;
using System.Drawing;
using ImpossibleLearning.Game;
using ImpossibleLearning.Levels;
using ImpossibleLearning.Render;
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
            using (var game = new BasicRenderer())
			{
				// Run the game at 60 updates per second
				game.Run(60.0);
                game.Exit();
			}
            
            // Console.ReadKey(true);
		}
	}
}
