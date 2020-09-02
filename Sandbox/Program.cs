using System;
using System.Collections.Generic;
using ECS;
using static ECS.Skogix;

namespace Sandbox
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			ECS.Skogix.Init();
			
			var skogix = new Entity(new Skogix());
			
			Print(skogix.Info);
			Print(skogix.Get<TestComponent>().MyString);
		}

		private static void Print(object msg) => Console.WriteLine($"{msg.ToString()}");

		public class Skogix : ITemplate
		{
			public Skogix()
			{
				Components.Add(new TestComponent("huhu"));
			}
			public string Name { get; } = "Skogix";
			public List<Component> Components { get; set; } = new List<Component>();
		}

		public sealed class TestComponent : Component
		{
			public TestComponent(string myString)
			{
				MyString = myString;
			}

			public string MyString { get; set; }
		}
	}
}