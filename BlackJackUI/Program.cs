using System;
using System.Reflection.PortableExecutable;
using CardGame;
using ECS;

namespace BlackJackUI
{
	class Program
	{
		public static int GameWidth = 120;
		public static int GameHeight = 10;

		static void Main(string[] args)
		{
			Game.Run(GameWidth, GameHeight);
		}


		internal static void PrintToConsole(in int x, in int y, in string message, ConsoleColor color)
		{
			var textArray = message.ToCharArray();
			for (int i = 0; i < textArray.Length; i++)
			{
				Console.SetCursorPosition(x+i, y);
				Console.ForegroundColor = color;
				Console.Write(textArray[i]);
				Console.ForegroundColor = ConsoleColor.White;
			}
		}
	}
}