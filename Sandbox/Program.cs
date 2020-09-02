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
			var templatedEntity = new Entity(new SkogixTemplate());
			

			Console.WriteLine(templatedEntity._componentsByType.Count);
			Console.WriteLine(templatedEntity.Get<TestComponent>().SomeText);
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