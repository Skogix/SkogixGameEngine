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
			//HubPub.Run();
			Init();
			var testFilter = new Filter(typeof(TestComponent), typeof(TestComponent2));
			testFilter.AddFilter<TestComponent>();

			var skogix = new Entity();
			//skogix.Add(new TestComponent("mytext"), new TestComponent2("huhu"));
			skogix.Add(new TestComponent("test"));
			skogix.Add(new TestComponent2("test"));
			Print(skogix.Info);
			
			Print(testFilter.Entities.Count);
			
		}

		private static void Print(object msg) => Console.WriteLine($"{msg.ToString()}");

		public sealed class TestComponent : Component
		{
			public string SomeText { get; }

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
				output.Add(new TestComponent2("MyText2"));
				return output;
			}
		}
	}
}