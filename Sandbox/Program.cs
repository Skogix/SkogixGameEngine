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

			var components = new List<Component>();
			components.Add(new TestComponent());
			var entity = new Entity(components);
			var entity2 = new Entity();
			entity2.Add(new TestComponent());
			var testComponent = entity2.Get<TestComponent>();
			testComponent.SomeText = "huhu";
			Console.WriteLine(entity2.Get<TestComponent>().SomeText);

		}

		public sealed class TestComponent : Component
		{
			public string SomeText { get; set; }
		}
	}
}