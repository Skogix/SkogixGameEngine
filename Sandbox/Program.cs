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
			Hub.Sub<ComponentAddedEvent>(skogix, e => Console.WriteLine($"Skogix har lyssnat på {e.Entity.Hash} + {e.ComponentType.Name}"));
			
			
			skogix.Add(new TestComponent("huhu"));
			
		}

		public sealed class TestComponent : Component
		{
			public string SomeText { get; set; }

			public TestComponent(string someText)
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