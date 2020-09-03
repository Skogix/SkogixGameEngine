using System;
using System.Diagnostics;
using CardGame.Systems;
using ECS;

public enum Eui
{
	None,
	Print,
	SetFixed,
}
namespace CardGame.Events
{
	public static class Send
	{
		public static void Ui(E e, int x, int y, string msg, ConsoleColor color) => Hub.Pub(new UiEvent(e, x, y, msg, color));
	}

	public enum E
	{
		None,
		Print,
		
	}
	public class UiEvent : IEvent
	{
		public int X;
		public int Y;
		public string Msg;
		public ConsoleColor Color;
		public E E;
		public UiEvent(E e, in int x, in int y, string msg, ConsoleColor color)
		{
			E = e;
			X = x;
			Y = y;
			Msg = msg;
			Color = color;
		}
	}
}