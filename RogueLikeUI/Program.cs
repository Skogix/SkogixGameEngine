#region
using ECS;
using RogueLike;
#endregion

namespace RogueLikeUI {
	internal class Program {
		private static void Main(string[] args) {
			var w = new World();
			
			w.AddSystem(new InputSystem(w));
			w.AddSystem(new MapSystem(w));
			w.AddSystem(new DrawSystem(w));
			w.InitSystems();
			
			while (true) w.Run();
		}
	}
}