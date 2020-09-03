using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CardGame.Events;
using ECS;

namespace CardGame.Systems
{
	public enum UiPosition{
		None,
		DealerText,

		PlayerText
	}

	public enum UiCommand
	{
		None,
		ClearScreen,
		TogglePermanent,
		SetUiPositionText,

		Print
	}
	public class UiSystem : EntitySystem, RunSystem, InitSystem
	{
		public void Run()
		{
		}

		public void Init()
		{
			Send.UiSetUiPositionText(UiPosition.DealerText, "Dealer:");
		}
	}

	public class Ui : IEvent
	{
		public Transform Transform;
		public string Message;
		public bool IsPermanent;
		public ConsoleColor Color;
		public UiCommand UiCommand = UiCommand.None;
		public UiPosition UiPosition = UiPosition.None;
		
		public Ui(UiCommand uiCommand, Transform transform, string message = "", in bool isPermanent = false, ConsoleColor color = ConsoleColor.White)
		{
			UiCommand = uiCommand;
			Transform = transform;
			Message = message;
			IsPermanent = isPermanent;
			Color = color;
		}

		public Ui(UiCommand uiCommand, int x, int y, string message, bool isPermanent = false, ConsoleColor color = ConsoleColor.White) : this(uiCommand, new Transform(x,y), message, isPermanent, color)
		{
		}

		public Ui(UiCommand uiCommand, UiPosition uiPosition, string message)
		{
			UiCommand = uiCommand;
			UiPosition = uiPosition;
			Message = message;
		}
	}
}