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
			var firstEntity = new Entity(new TestComponent("mytext"));
			var secondEntity = new Entity(firstEntity);

			Console.WriteLine(secondEntity._componentsByType.Count);
			Console.WriteLine(secondEntity.Get<TestComponent>().SomeText);
		}

		public sealed class TestComponent : Component
		{
			public string SomeText { get; set; }

			public TestComponent(string someText)
			{
				SomeText = someText;
			}
		}
	}
}