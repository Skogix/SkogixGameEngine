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
		}

		public sealed class TestComponent : Component
		{
			public string SomeText { get; set; }
		}
	}
}