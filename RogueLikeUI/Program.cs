#region
using ECS;
using RogueLike;
using RogueLike.Systems;
#endregion

namespace RogueLikeUI {
	internal class Program {
		private static void Main(string[] args) {
			var w = new World();
			w.AddSystem(new MapSystem(w));
			
			w.AddSystem(new InputSystem(w));
			w.AddSystem(new MoveSystem(w));
			w.AddSystem(new DrawSystem(w));
			
			w.InitSystems();
			
			var skogix = w.CreateEntity(new PlayerTemplate("Skogix"));
			while (true) w.Run();
		}
	}
}