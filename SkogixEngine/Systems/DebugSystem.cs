using System;

namespace ECS {
	public class DebugSystem{
		public World World { get; }
		public DebugSystem(World world) {
			World = world; 
		}
		public void Debug(string message) {
			Console.WriteLine($"DEBUG::: {message}");
		}
		
		
	}
}