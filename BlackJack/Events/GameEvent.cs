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
	public static class Send<TEvent> where TEvent : IEvent
	{
		public static void Debug(string message) => Hub.Pub(new Debug(message));
		public static void Ui(Eui eui, int x, int y, string msg) => Hub.Pub(new GameEvent<UiSystem, IPrint>());
	}

	public interface IPrint : IEvent { }

	public class Send<TEvent, TSystem> 
		where TEvent : IEvent
		where TSystem : EntitySystem
	{
		
	}

	public class GameEvent<TS, TE>
		where TS : EntitySystem
		where TE : IEvent
	{
		
	}
	public class GameEvent<TEvent> 
		where TEvent : IEvent
	{
		public string Message;
		public GameEvent(string message)
		{
			Message = message;
		}
	}

	public class Debug : IEvent
	{
		public string Message;

		public Debug(string message)
		{
			Message = message;
		}
	}
}