using System;
using System.Collections.Generic;
using ECS;

namespace Sandbox
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			//HubPub.Run();
			Skogix.Init();
			
			var skogix = new Entity();
			var testFilter = new Filter();
			
			testFilter.AddFilter<TestComponent2>();
			testFilter.AddFilter<TestComponent>();
			
			skogix.Add(new TestComponent2("wawa"));
			skogix.Add(new TestComponent("huhu"));

			testFilter.Entities.ForEach(Print);
			
			skogix.Remove(skogix.Get<TestComponent2>());
			
			testFilter.Entities.ForEach(Print);
			
			
		}

		private static void Print(Entity entity) => Console.WriteLine($"{entity.Hash}");

		public sealed class TestComponent : Component
		{
			public string SomeText { get; set; }

			public TestComponent(string someText)
			{
				SomeText = someText;
			}
		}

		public sealed class TestComponent2 : Component
		{
			public string SomeText { get; set; }

			public TestComponent2(string someText)
			{
				SomeText = someText;
			}
		}
		public class SkogixTemplate : ITemplate
		{
			public string Name { get; } = "SkogixTemplate";
			public IEnumerable<Component> Components()
			{
				var output = new List<Component>();
				output.Add(new TestComponent("MyText"));
				return output;
			}
		}
	}
}