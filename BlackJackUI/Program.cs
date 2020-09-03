using System;
using System.Collections.Generic;
using System.Linq;
using CardGame;
using CardGame.Systems;
using ECS;

namespace BlackJackUI
{
	class Program
	{
		public static int GameWidth = 120;
		public static int GameHeight = 10;
		internal static Dictionary<UiPosition, Transform> UiPositionsToTransform { get; set; }= new Dictionary<UiPosition, Transform>()
		{
			{UiPosition.None, new Transform(0,0)},
			{UiPosition.DealerText, new Transform(2,1)},
		};

		internal static Dictionary<UiPosition, string> UiPositionsToText { get; set; } = new Dictionary<UiPosition, string>();

		static void Main(string[] args)
		{
			Hub.Sub<Ui>(null, OnUi);
			foreach (UiPosition uiPosition in Enum.GetValues(typeof(UiPosition)))
				UiPositionsToText.Add(uiPosition, "");
			Game.Run(GameWidth, GameHeight);
		}


		private static void OnUi(Ui e)
		{
			switch (e.UiCommand)
			{
				case UiCommand.None:
					break;
				case UiCommand.ClearScreen: 
					break;
				case UiCommand.TogglePermanent:
					break;
				case UiCommand.SetUiPositionText: SetPositionText(e.UiPosition, e.Message);
					break;
				case UiCommand.Print: PrintToConsole(e.Transform.X, e.Transform.Y, e.Message, e.Color);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			PrintPermanentTiles();
		}

		private static void SetPositionText(UiPosition uiPosition, string message)
		{
			UiPositionsToText[uiPosition] = message;
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

		private static void PrintPermanentTiles()
		{
			foreach (var vp in UiPositionsToText.Where(t => t.Value != ""))
			{
				var transform = UiPositionsToTransform[vp.Key];
				PrintToConsole(transform.X, transform.Y, vp.Value, ConsoleColor.White);
			}
		}
	}
}