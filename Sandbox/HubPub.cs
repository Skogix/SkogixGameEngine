using System;
using System.Linq;
using ECS;

namespace Sandbox
{
	public static class HubPub
	{
		public static void Run()
		{
			var skogix = new Person("Skogix");

			Bus.Push(new TestEvent(skogix, "TestEvent"));
			Bus.Push(new TestActionEvent(PrintActionEvent));
			Bus.Push(new TestFuncEvent(PrintFuncEvent));

			Bus.Pull<TestEvent>().ToList().ForEach(e => Print(e.Message));
			Bus.Pull<TestActionEvent>().ToList().ForEach(e => e.Action.Invoke());
			Bus.Pull<TestFuncEvent>().ToList().ForEach(e => Print(e.Func.Invoke()));

			Hub.Sub<TestEvent>(skogix, e => Print(e.Message));
			Hub.Sub<TestActionEvent>(skogix, e => e.Action.Invoke());
			Hub.Sub<TestFuncEvent>(skogix, e => Print(e.Func.Invoke()));

			Hub.Pub(skogix, new TestEvent(skogix, "TestEvent"));
			Hub.Pub(skogix, new TestActionEvent(PrintActionEvent));
			Hub.Pub(skogix, new TestFuncEvent(PrintFuncEvent));
		}

		private static string PrintFuncEvent()
		{
			return "PrintFuncEvent";
		}

		private static void PrintActionEvent()
		{
			Print("PrintActionEvent");
		}

		private static void Br()
		{
			Console.WriteLine();
		}

		private static void Hr(string msg = "")
		{
			Console.WriteLine($"-----{msg}");
		}

		private static void Tab()
		{
			Console.WriteLine("\t");
		}

		private static void Print(string msg)
		{
			Console.WriteLine(msg);
		}
	}

	internal class Person
	{
		public readonly string Name;

		public Person(string name)
		{
			Name = name;
		}
	}

	internal class TestActionEvent
	{
		public Action Action;

		public TestActionEvent(Action action)
		{
			Action = action;
		}
	}

	internal class TestFuncEvent
	{
		public Func<string> Func;

		public TestFuncEvent(Func<string> func)
		{
			Func = func;
		}
	}

	internal class TestEvent
	{
		public string Message;
		public Person Sender;

		public TestEvent(Person sender, string message)
		{
			Sender = sender;
			Message = message;
		}
	}
}